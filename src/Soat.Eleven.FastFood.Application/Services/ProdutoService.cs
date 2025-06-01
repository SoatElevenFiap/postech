using Soat.Eleven.FastFood.Application.DTOs.Produto;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IRepository<CategoriaProduto> _categoriaRepository;
        private readonly ILogger<ProdutoService> _logger;
        private readonly IImagemService _imageService;
        private const string DIRETORIO_IMAGENS = "produtos";

        public ProdutoService(
            IRepository<Produto> produtoRepository, 
            IRepository<CategoriaProduto> categoriaRepository,
            ILogger<ProdutoService> logger,
            IImagemService imageService)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _logger = logger;
            _imageService = imageService;
        }

        private async Task<string> ObterUrlCompleta(string? nomeImagem)
        {
            return await _imageService.ObterUrlImagemAsync(DIRETORIO_IMAGENS, nomeImagem);
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarProdutos(bool? incluirInativos = false, Guid? categoryId = null)
        {
            IEnumerable<Produto> produtos;

            if (categoryId.HasValue)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(categoryId.Value);
                if (categoria == null)
                    throw new ArgumentException("Categoria não encontrada");

                produtos = incluirInativos == true
                    ? await _produtoRepository.FindAsync(p => p.CategoriaId == categoryId.Value)
                    : await _produtoRepository.FindAsync(p => p.CategoriaId == categoryId.Value && p.Ativo);
            }
            else
            {
                produtos = incluirInativos == true
                    ? await _produtoRepository.GetAllAsync()
                    : await _produtoRepository.FindAsync(p => p.Ativo);
            }

            var produtosDTO = new List<ProdutoDTO>();
            foreach (var produto in produtos)
            {
                produtosDTO.Add(new ProdutoDTO
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    SKU = produto.SKU,
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    CategoriaId = produto.CategoriaId,
                    Ativo = produto.Ativo,
                    CriadoEm = produto.CriadoEm,
                    Imagem = await ObterUrlCompleta(produto.Imagem)
                });
            }

            return produtosDTO;
        }

        public async Task<ProdutoDTO?> ObterProdutoPorId(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                return null;

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                SKU = produto.SKU,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                Ativo = produto.Ativo,
                CriadoEm = produto.CriadoEm,
                Imagem = await ObterUrlCompleta(produto.Imagem)
            };
        }

        public async Task<ProdutoDTO> CriarProduto(ProdutoDTO produto)
        {
            if (produto.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero");

            var existeProduto = await _produtoRepository.FindAsync(p => p.SKU == produto.SKU);
            if (existeProduto.Any())
                throw new ArgumentException("Produto com mesmo SKU já existe");

            var categoria = await _categoriaRepository.GetByIdAsync(produto.CategoriaId);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            var novoProduto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = produto.Nome,
                SKU = produto.SKU,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                Ativo = true,
                CriadoEm = DateTime.UtcNow,
                Imagem = _imageService.GerarNomeArquivo(produto.Imagem ?? string.Empty)
            };

            var produtoCriado = await _produtoRepository.AddAsync(novoProduto);

            return new ProdutoDTO
            {
                Id = produtoCriado.Id,
                Nome = produtoCriado.Nome,
                SKU = produtoCriado.SKU,
                Descricao = produtoCriado.Descricao,
                Preco = produtoCriado.Preco,
                CategoriaId = produtoCriado.CategoriaId,
                Ativo = produtoCriado.Ativo,
                CriadoEm = produtoCriado.CriadoEm,
                Imagem = await ObterUrlCompleta(produtoCriado.Imagem)
            };
        }

        public async Task<ProdutoDTO> AtualizarProduto(Guid id, AtualizarProdutoDTO produto)
        {
            _logger.LogInformation("Atualizando produto: {Id}", id);
            _logger.LogInformation("Imagem enviada: {Imagem}", produto.ImagemFoiEnviada());

            if (produto.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero");

            var produtoExistente = await _produtoRepository.GetByIdAsync(id);
            if (produtoExistente == null)
                throw new ArgumentException("Produto não encontrado");

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco ?? produtoExistente.Preco;
            

            await _produtoRepository.UpdateAsync(produtoExistente);

            return new ProdutoDTO
            {
                Id = produtoExistente.Id,
                Nome = produtoExistente.Nome,
                SKU = produtoExistente.SKU,
                Descricao = produtoExistente.Descricao,
                Preco = produtoExistente.Preco,
                CategoriaId = produtoExistente.CategoriaId,
                Ativo = produtoExistente.Ativo,
                CriadoEm = produtoExistente.CriadoEm,
                Imagem = await ObterUrlCompleta(produtoExistente.Imagem)
            };
        }

        public async Task DesativarProduto(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            produto.Ativo = false;
            await _produtoRepository.UpdateAsync(produto);
        }

        public async Task ReativarProduto(Guid id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");
            
            produto.Ativo = true;
            await _produtoRepository.UpdateAsync(produto);
        }

        public async Task<string> UploadImagemAsync(Guid produtoId, ImagemUploadDTO imagem)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            var nomeArquivo = await _imageService.UploadImagemAsync(DIRETORIO_IMAGENS, produtoId.ToString(), imagem);
            produto.Imagem = nomeArquivo;
            await _produtoRepository.UpdateAsync(produto);
            return nomeArquivo;
        }

        public async Task RemoverImagemAsync(Guid produtoId)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            await _imageService.RemoverImagemAsync(DIRETORIO_IMAGENS, produtoId.ToString());
            produto.Imagem = null;
            await _produtoRepository.UpdateAsync(produto);
        }
    }
} 
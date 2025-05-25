using Soat.Eleven.FastFood.Application.DTOs.Produto;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IRepository<CategoriaProduto> _categoriaRepository;

        public ProdutoService(IRepository<Produto> produtoRepository, IRepository<CategoriaProduto> categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
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

            return produtos.Select(p => new ProdutoDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                SKU = p.SKU,
                Descricao = p.Descricao,
                Preco = p.Preco,
                CategoriaId = p.CategoriaId,
                Ativo = p.Ativo,
                CriadoEm = p.CriadoEm,
            });
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
                CriadoEm = DateTime.UtcNow
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
            };
        }

        public async Task<ProdutoDTO> AtualizarProduto(Guid id, AtualizarProdutoDTO produto)
        {
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
    }
} 
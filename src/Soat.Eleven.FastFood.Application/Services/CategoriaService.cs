using Soat.Eleven.FastFood.Application.DTOs.Categoria;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IRepository<CategoriaProduto> _categoriaRepository;

        public CategoriaService(IRepository<CategoriaProduto> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaDTO>> ListarCategorias(bool? incluirInativos = false)
        {
            var categorias = incluirInativos == true ? await _categoriaRepository.GetAllAsync() : await _categoriaRepository.FindAsync(c => c.Ativo);
            return categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Ativo = c.Ativo
            });
        }

        public async Task<CategoriaDTO?> ObterCategoriaPorId(Guid id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
                return null;

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo
            };
        }

        public async Task<CategoriaDTO> CriarCategoria(CategoriaDTO categoria)
        {
            var existeCategoria = await _categoriaRepository.FindAsync(c => c.Nome == categoria.Nome);
            if (existeCategoria.Any())
                throw new ArgumentException("Categoria de mesmo nome já existe");
            
            var novaCategoria = new CategoriaProduto
            {
                Id = Guid.NewGuid(),
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = true
            };

            var categoriaCriada = await _categoriaRepository.AddAsync(novaCategoria);

            return new CategoriaDTO
            {
                Id = categoriaCriada.Id,
                Nome = categoriaCriada.Nome,
                Descricao = categoriaCriada.Descricao,
                Ativo = categoriaCriada.Ativo
            };
        }

        public async Task<CategoriaDTO> AtualizarCategoria(Guid id, CategoriaDTO categoria)
        {
            var categoriaExistente = await _categoriaRepository.GetByIdAsync(id);
            if (categoriaExistente == null)
                throw new ArgumentException("Categoria não encontrada");

            categoriaExistente.Nome = categoria.Nome;
            categoriaExistente.Descricao = categoria.Descricao;

            await _categoriaRepository.UpdateAsync(categoriaExistente);

            return new CategoriaDTO
            {
                Id = categoriaExistente.Id,
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao,
                Ativo = categoriaExistente.Ativo
            };
        }

        public async Task DesativarCategoria(Guid id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");
            categoria.Ativo = false;
            await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task ReativarCategoria(Guid id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");
            
            categoria.Ativo = true;
            await _categoriaRepository.UpdateAsync(categoria);
        }
    }
} 
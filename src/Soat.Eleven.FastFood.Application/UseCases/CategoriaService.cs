using Soat.Eleven.FastFood.Domain.UseCases;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Categoria;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        private readonly ICategoriaGateway _categoriaGateway;

        public CategoriaUseCase(ICategoriaGateway categoriaGateway)
        {
            _categoriaGateway = categoriaGateway;
        }

        public async Task<IEnumerable<ResumoCategoria>> ListarCategorias(bool? incluirInativos = false)
        {
            var categorias = incluirInativos == true ? 
                await _categoriaGateway.GetAllAsync() : 
                await _categoriaGateway.FindAsync(c => c.Ativo);
            
            return categorias.Select(c => new ResumoCategoria
            {
                Id = c.Id,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Ativo = c.Ativo
            });
        }

        public async Task<ResumoCategoria?> ObterCategoriaPorId(Guid id)
        {
            var categoria = await _categoriaGateway.GetByIdAsync(id);
            if (categoria == null)
                return null;

            return new ResumoCategoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo
            };
        }

        public async Task<ResumoCategoria> CriarCategoria(ResumoCategoria categoria)
        {
            var existeCategoria = await _categoriaGateway.FindAsync(c => c.Nome == categoria.Nome);
            if (existeCategoria.Any())
                throw new ArgumentException("Categoria de mesmo nome já existe");
            
            var novaCategoria = new CategoriaProduto
            {
                Id = Guid.NewGuid(),
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = true
            };

            var categoriaCriada = await _categoriaGateway.AddAsync(novaCategoria);

            return new ResumoCategoria
            {
                Id = categoriaCriada.Id,
                Nome = categoriaCriada.Nome,
                Descricao = categoriaCriada.Descricao,
                Ativo = categoriaCriada.Ativo
            };
        }

        public async Task<ResumoCategoria> AtualizarCategoria(Guid id, ResumoCategoria categoria)
        {
            var categoriaExistente = await _categoriaGateway.GetByIdAsync(id);
            if (categoriaExistente == null)
                throw new ArgumentException("Categoria não encontrada");

            categoriaExistente.Nome = categoria.Nome;
            categoriaExistente.Descricao = categoria.Descricao;

            await _categoriaGateway.UpdateAsync(categoriaExistente);

            return new ResumoCategoria
            {
                Id = categoriaExistente.Id,
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao,
                Ativo = categoriaExistente.Ativo
            };
        }

        public async Task DesativarCategoria(Guid id)
        {
            var categoria = await _categoriaGateway.GetByIdAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");
            categoria.Ativo = false;
            await _categoriaGateway.UpdateAsync(categoria);
        }

        public async Task ReativarCategoria(Guid id)
        {
            var categoria = await _categoriaGateway.GetByIdAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");
            
            categoria.Ativo = true;
            await _categoriaGateway.UpdateAsync(categoria);
        }
    }
} 
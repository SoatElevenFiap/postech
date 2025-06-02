using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface ICategoriaService<Categoria>where Categoria: IEntity 
    {
        Task<IEnumerable<Categoria>> ListarCategorias(bool? incluirInativos = false);
        Task<Categoria?> ObterCategoriaPorId(Guid id);
        Task<Categoria> CriarCategoria(Categoria categoria);
        Task<Categoria> AtualizarCategoria(Guid id, Categoria categoria);
        Task DesativarCategoria(Guid id);
        Task ReativarCategoria(Guid id);
    }
}
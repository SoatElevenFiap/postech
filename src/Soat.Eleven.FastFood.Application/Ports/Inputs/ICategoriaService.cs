using Soat.Eleven.FastFood.Application.DTOs.Categoria;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> ListarCategorias(bool? incluirInativos = false);
        Task<CategoriaDTO?> ObterCategoriaPorId(Guid id);
        Task<CategoriaDTO> CriarCategoria(CategoriaDTO categoria);
        Task<CategoriaDTO> AtualizarCategoria(Guid id, CategoriaDTO categoria);
        Task DesativarCategoria(Guid id);
        Task ReativarCategoria(Guid id);
    }
}
using Soat.Eleven.FastFood.Application.DTOs.Categoria;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface ICategoriaService
    {
        Task<IEnumerable<ResumoCategoria>> ListarCategorias(bool? incluirInativos = false);
        Task<ResumoCategoria?> ObterCategoriaPorId(Guid id);
        Task<ResumoCategoria> CriarCategoria(ResumoCategoria categoria);
        Task<ResumoCategoria> AtualizarCategoria(Guid id, ResumoCategoria categoria);
        Task DesativarCategoria(Guid id);
        Task ReativarCategoria(Guid id);
    }
}
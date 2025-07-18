using Soat.Eleven.FastFood.Core.Domain.Contratos.Categoria;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public interface ICategoriaUseCase
    {
        Task<IEnumerable<ResumoCategoria>> ListarCategorias(bool? incluirInativos = false);
        Task<ResumoCategoria?> ObterCategoriaPorId(Guid id);
        Task<ResumoCategoria> CriarCategoria(ResumoCategoria categoria);
        Task<ResumoCategoria> AtualizarCategoria(Guid id, ResumoCategoria categoria);
        Task DesativarCategoria(Guid id);
        Task ReativarCategoria(Guid id);
    }
}
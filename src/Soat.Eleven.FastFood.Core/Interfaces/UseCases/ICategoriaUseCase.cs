using Soat.Eleven.FastFood.Core.DTOs.Categorias;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface ICategoriaUseCase
{
    Task<IEnumerable<CategoriaProduto>> ListarCategorias(bool? incluirInativos = false);
    Task<CategoriaProduto?> ObterCategoriaPorId(Guid id);
    Task<CategoriaProduto> CriarCategoria(CategoriaProduto categoria);
    Task<CategoriaProduto> AtualizarCategoria(CategoriaProduto categoria);
    Task DesativarCategoria(Guid id);
    Task ReativarCategoria(Guid id);
}
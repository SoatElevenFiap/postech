using Soat.Eleven.FastFood.Common.DTOs.Categorias;

namespace Soat.Eleven.FastFood.Common.Interfaces.DataSources;

public interface ICategoriaProdutoDataSource
{
    Task<IEnumerable<CategoriaProdutoDto>> GetAllAsync();
    Task<CategoriaProdutoDto?> GetByIdAsync(Guid id);
    Task<CategoriaProdutoDto> AddAsync(CategoriaProdutoDto categoria);
    Task<CategoriaProdutoDto> UpdateAsync(CategoriaProdutoDto categoria);
    Task<IEnumerable<CategoriaProdutoDto>> FindAsync(Func<CategoriaProdutoDto, bool> predicate);
}

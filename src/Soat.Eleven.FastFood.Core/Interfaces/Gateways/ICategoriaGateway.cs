using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface ICategoriaGateway
{
    Task<IEnumerable<CategoriaProduto>> GetAllAsync();
    Task<CategoriaProduto?> GetByIdAsync(Guid id);
    Task<CategoriaProduto> AddAsync(CategoriaProduto categoria);
    Task<CategoriaProduto> UpdateAsync(CategoriaProduto categoria);
    Task<IEnumerable<CategoriaProduto>> FindAsync(Func<CategoriaProduto, bool> predicate);
}

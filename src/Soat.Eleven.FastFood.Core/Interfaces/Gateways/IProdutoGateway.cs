using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IProdutoGateway
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(Guid id);
    Task<IEnumerable<Produto>> FindAsync(Func<Produto, bool> predicate);
    Task<IEnumerable<Produto>> GetByCategoriaAsync(Guid categoriaId);
    Task AddAsync(Produto produto);
    Task UpdateAsync(Produto produto);
}

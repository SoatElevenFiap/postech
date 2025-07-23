namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IGateway<T>
{
    Task<T> AddAsync(T pedido);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task UpdateAsync(T pedido);
    Task DeleteAsync(T entity);
}

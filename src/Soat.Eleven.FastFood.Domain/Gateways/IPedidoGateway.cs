using Soat.Eleven.FastFood.Domain.Entidades;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface IPedidoGateway
    {
        Task<Pedido> AddAsync(Pedido entity);
        Task<Pedido?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<IEnumerable<Pedido>> FindAsync(Expression<Func<Pedido, bool>> predicate);
        Task UpdateAsync(Pedido entity);
        Task DeleteAsync(Pedido entity);
        Task<Pedido> SaveWithItemsAsync(Pedido pedido);
    }
}

using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IPedidoGateway
{
    Task<Pedido> AddAsync(Pedido pedido);
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido> GetByIdAsync(Guid id);
    Task UpdateAsync(Pedido pedido);
}

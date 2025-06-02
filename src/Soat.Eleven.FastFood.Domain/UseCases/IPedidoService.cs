using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPedidoService<Pedido> where Pedido : IEntity
    {
        Task<Pedido> CriarPedido(Pedido pedido);
        Task<Pedido> AtualizarPedido(Guid id, Pedido pedidoDto);
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task<Pedido?> ObterPedidoPorId(Guid id);
        Task<Pedido> PagarPedido(Guid id, Pedido pagamento);
        Task IniciarPreparacaoPedido(Guid id);
        Task FinalizarPreparacaoPedido(Guid id);
        Task FinalizarPedido(Guid id);
        Task CancelarPedido(Guid id);
    }
}

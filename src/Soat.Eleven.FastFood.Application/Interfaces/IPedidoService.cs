using Soat.Eleven.FastFood.Application.DTOs.Pedidos.Request;
using Soat.Eleven.FastFood.Application.DTOs.Pedidos.Response;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoResponseDto> CriarPedido(PedidoRequestDto pedido);
        Task<PedidoResponseDto> AtualizarPedido(Guid id, PedidoRequestDto pedidoDto);
        Task<IEnumerable<PedidoResponseDto>> ListarPedidos();
        Task<PedidoResponseDto?> ObterPedidoPorId(Guid id);
        Task IniciarPreparacaoPedido(Guid id);
        Task FinalizarPreparacaoPedido(Guid id);
        Task FinalizarPedido(Guid id);
        Task CancelarPedido(Guid id);
    }
}

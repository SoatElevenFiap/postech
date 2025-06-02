using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Request;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Response;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPedidoService
    {
        Task<PedidoResponseDto> CriarPedido(PedidoRequestDto pedido);
        Task<PedidoResponseDto> AtualizarPedido(Guid id, PedidoRequestDto pedidoDto);
        Task<IEnumerable<PedidoResponseDto>> ListarPedidos();
        Task<PedidoResponseDto?> ObterPedidoPorId(Guid id);
        Task<ConfirmacaoPagamento> PagarPedido(Guid id, SolicitacaoPagamento pagamento);
        Task IniciarPreparacaoPedido(Guid id);
        Task FinalizarPreparacaoPedido(Guid id);
        Task FinalizarPedido(Guid id);
        Task CancelarPedido(Guid id);
    }
}

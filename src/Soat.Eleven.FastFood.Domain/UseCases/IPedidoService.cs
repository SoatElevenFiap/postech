using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPedidoService
    {
        Task<PedidoOutput> CriarPedido(PedidoInput pedido);
        Task<PedidoOutput> AtualizarPedido(Guid id, PedidoInput pedidoDto);
        Task<IEnumerable<PedidoOutput>> ListarPedidos();
        Task<PedidoOutput?> ObterPedidoPorId(Guid id);
        Task<ConfirmacaoPagamento> PagarPedido(Guid id, SolicitacaoPagamento pagamento);
        Task IniciarPreparacaoPedido(Guid id);
        Task FinalizarPreparacaoPedido(Guid id);
        Task FinalizarPedido(Guid id);
        Task CancelarPedido(Guid id);
    }
}

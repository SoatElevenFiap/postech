using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs;

public interface IPedidoUseCase
{
    Task<PedidoOutput> CriarPedido(PedidoInput pedidoDto);
    Task<PedidoOutput> AtualizarPedido(Guid id, PedidoInput pedidoDto);
    Task<IEnumerable<PedidoOutput>> ListarPedidos();
    Task<PedidoOutput?> ObterPedidoPorId(Guid id);
    Task IniciarPreparacaoPedido(Guid id);
    Task FinalizarPreparacaoPedido(Guid id);
    Task FinalizarPedido(Guid id);
    Task CancelarPedido(Guid id);
    Task<ConfirmacaoPagamento> PagarPedido(Guid id, SolicitacaoPagamento pagamento);
}
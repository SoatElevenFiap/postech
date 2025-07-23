using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;
public interface IPedidoUseCase
{
    Task<Pedido> CriarPedido(Pedido pedidoDto);
    Task<Pedido> AtualizarPedido(Pedido pedidoDto);
    Task<IEnumerable<Pedido>> ListarPedidos();
    Task<Pedido?> ObterPedidoPorId(Guid id);
    Task IniciarPreparacaoPedido(Guid id);
    Task FinalizarPreparacaoPedido(Guid id);
    Task FinalizarPedido(Guid id);
    Task CancelarPedido(Guid id);
    Task<ConfirmacaoPagamento> PagarPedido(SolicitacaoPagamento solicitacaoPagamento, IPagamentoGateway pagamentoGateway);
}
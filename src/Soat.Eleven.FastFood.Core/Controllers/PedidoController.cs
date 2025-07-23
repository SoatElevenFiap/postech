using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class PedidoController
{
    private readonly IPedidoGateway pedidoGateway;

    public PedidoController(IPedidoGateway pedidoGateway)
    {
        this.pedidoGateway = pedidoGateway;
    }

    public async Task<PedidoOutputDto> CriarPedido(PedidoInputDto inputDto)
    {
        var entity = PedidoPresenter.Input(inputDto);
        var useCase = new PedidoUseCase(pedidoGateway);
        var result = await useCase.CriarPedido(entity);

        return PedidoPresenter.Output(result);
    }

    public async Task<PedidoOutputDto> AtualizarPedido(PedidoInputDto inputDto)
    {
        var entity = PedidoPresenter.Input(inputDto);
        var useCase = new PedidoUseCase(pedidoGateway);
        var result = await useCase.AtualizarPedido(entity);

        return PedidoPresenter.Output(result);
    }

    public async Task<IEnumerable<PedidoOutputDto>> ListarPedidos()
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        var result = await useCase.ListarPedidos();

        return result.Select(PedidoPresenter.Output);
    }

    public async Task<PedidoOutputDto> ObterPedidoPorId(Guid id)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        var result = await useCase.ObterPedidoPorId(id);

        return PedidoPresenter.Output(result);
    }

    public async Task<ConfirmacaoPagamento> PagarPedido(SolicitacaoPagamento solicitacaoPagamento, IPagamentoGateway pagamentoGateway)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        return await useCase.PagarPedido(solicitacaoPagamento, pagamentoGateway);
    }

    public async Task IniciarPreparacaoPedido(Guid id)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        await useCase.IniciarPreparacaoPedido(id);
    }

    public async Task FinalizarPreparacao(Guid id)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        await useCase.FinalizarPreparacaoPedido(id);
    }

    public async Task FinalizarPedido(Guid id)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        await useCase.FinalizarPedido(id);
    }

    public async Task CancelarPedido(Guid id)
    {
        var useCase = new PedidoUseCase(pedidoGateway);
        await useCase.CancelarPedido(id);
    }
}

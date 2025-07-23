using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class PedidoUseCase : IPedidoUseCase
{
    private readonly IPedidoGateway _pedidoGateway;

    public PedidoUseCase(IPedidoGateway pedidoGateway)
    {
        _pedidoGateway = pedidoGateway;
    }

    public async Task<Pedido> CriarPedido(Pedido pedido)
    {
        pedido.GerarSenha();

        pedido = await _pedidoGateway.AddAsync(pedido);

        return pedido;
    }

    public async Task<Pedido> AtualizarPedido(Guid id, Pedido pedidoDto)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.Pendente)
            throw new Exception($"O status do pedido não permite alteração.");

        pedido.TokenAtendimentoId = pedidoDto.TokenAtendimentoId;
        pedido.ClienteId = pedidoDto.ClienteId;
        pedido.Subtotal = pedidoDto.Subtotal;
        pedido.Desconto = pedidoDto.Desconto;
        pedido.Total = pedidoDto.Total;

        pedido.Itens.Clear();

        var novosItens = pedidoDto.Itens.ToList() ?? [];
        pedido.AdicionarItens(novosItens);

        await _pedidoGateway.UpdateAsync(pedido);

        return pedido;
    }

    public async Task<IEnumerable<Pedido>> ListarPedidos()
    {
        var pedidos = await _pedidoGateway.GetAllAsync();

        return pedidos;
    }

    public async Task<Pedido?> ObterPedidoPorId(Guid id)
    {
        var pedido = await _pedidoGateway.GetByIdAsync(id);

        if (pedido == null)
            return null;

        return pedido;
    }

    public async Task IniciarPreparacaoPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.Recebido)
            throw new Exception($"O status do pedido não permite iniciar a preparação. Status atual: {pedido.Status} ");

        pedido.Status = StatusPedido.EmPreparacao;

        await _pedidoGateway.UpdateAsync(pedido);
    }

    public async Task FinalizarPreparacaoPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.EmPreparacao)
            throw new Exception($"O status do pedido não está permite finalizar a preparação. Status atual: {pedido.Status} ");

        pedido.Status = StatusPedido.Pronto;

        await _pedidoGateway.UpdateAsync(pedido);
    }

    public async Task FinalizarPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.Pronto)
            throw new Exception($"O status do pedido não permite finalização. Status atual: {pedido.Status} ");

        pedido.Status = StatusPedido.Finalizado;

        await _pedidoGateway.UpdateAsync(pedido);
    }

    public async Task CancelarPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status == StatusPedido.Finalizado)
            throw new Exception($"Não é permitido cancelar pedido finalizado");

        pedido.Status = StatusPedido.Cancelado;

        await _pedidoGateway.UpdateAsync(pedido);
    }

    private async Task<Pedido> LocalizarPedido(Guid id)
    {
        var pedido = await _pedidoGateway.GetByIdAsync(id);

        return pedido ?? throw new Exception("Pedido não encontrado.");
    }

    public async Task<ConfirmacaoPagamento> PagarPedido(Guid id, TipoPagamento tipoPagamento, decimal value, IPagamentoGateway pagamentoGateway)
    {
        var pedido = await LocalizarPedido(id);

        var pagamentoProcessado = await pagamentoGateway.ProcessarPagamentoAsync(tipoPagamento, value);

        if (pedido.Status != StatusPedido.Pendente)
            throw new Exception($"O status do pedido não permite pagamento.");

        if (pedido.Total != value)
            throw new Exception($"Valor de pagamento difere do valor do pedido.");

        if (pagamentoProcessado.Status == StatusPagamento.Aprovado)
        {
            pedido.Status = StatusPedido.Recebido;
        }

        pedido.AdicionarPagamento(new PagamentoPedido(tipoPagamento, value, pagamentoProcessado.Status, pagamentoProcessado.Autorizacao));

        await _pedidoGateway.UpdateAsync(pedido);

        return pagamentoProcessado;
    }
}

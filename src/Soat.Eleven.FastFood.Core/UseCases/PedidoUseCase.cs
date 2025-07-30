﻿using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class PedidoUseCase
{
    private readonly PedidoGateway _pedidoGateway;

    private PedidoUseCase(PedidoGateway pedidoGateway)
    {
        _pedidoGateway = pedidoGateway;
    }

    public static PedidoUseCase Create(PedidoGateway pedidoGateway)
    {
        return new PedidoUseCase(pedidoGateway);
    }

    public async Task<Pedido> CriarPedido(PedidoInputDto pedidoDto)
    {
        var pedido = new Pedido
        {
            TokenAtendimentoId = pedidoDto.TokenAtendimentoId,
        };

        pedido.GerarSenha();

        pedido = await _pedidoGateway.CriarPedido(pedido);

        return pedidoDto;
    }

    public async Task<Pedido> AtualizarPedido(PedidoInputDto pedidoDto)
    {
        var pedido = await LocalizarPedido(pedidoDto.Id);

        if (pedido.Status != StatusPedido.Pendente)
            throw new Exception($"O status do pedido não permite alteração.");

        pedido.TokenAtendimentoId = pedidoDto.TokenAtendimentoId;
        pedido.ClienteId = pedidoDto.ClienteId;
        pedido.Subtotal = pedidoDto.Subtotal;
        pedido.Desconto = pedidoDto.Desconto;
        pedido.Total = pedidoDto.Total;

        pedido.Itens.Clear();


        var novosItens = pedidoDto.Itens.Select(i => new Core.Entities.ItemPedido
        {
            ProdutoId = i.ProdutoId,
            Quantidade = i.Quantidade,
            DescontoUnitario = i.DescontoUnitario,
            PrecoUnitario = i.PrecoUnitario
        }).ToList();

        pedido.AdicionarItens(novosItens);
        await _pedidoGateway.AtualizarPedido(pedido);

        return pedido;
    }

    public async Task<IEnumerable<Pedido>> ListarPedidos()
    {
        var pedidos = await _pedidoGateway.ListarPedidos();

        return pedidos;
    }

    public async Task<Pedido?> ObterPedidoPorId(Guid id)
    {
        var pedido = await _pedidoGateway.ObterPedidoPorId(id);

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

        await _pedidoGateway.AtualizarPedido(pedido);
    }

    public async Task FinalizarPreparacaoPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.EmPreparacao)
            throw new Exception($"O status do pedido não está permite finalizar a preparação. Status atual: {pedido.Status} ");

        pedido.Status = StatusPedido.Pronto;

        await _pedidoGateway.AtualizarPedido(pedido);
    }

    public async Task FinalizarPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status != StatusPedido.Pronto)
            throw new Exception($"O status do pedido não permite finalização. Status atual: {pedido.Status} ");

        pedido.Status = StatusPedido.Finalizado;

        await _pedidoGateway.AtualizarPedido(pedido);
    }

    public async Task CancelarPedido(Guid id)
    {
        var pedido = await LocalizarPedido(id);

        if (pedido.Status == StatusPedido.Finalizado)
            throw new Exception($"Não é permitido cancelar pedido finalizado");

        pedido.Status = StatusPedido.Cancelado;

        await _pedidoGateway.AtualizarPedido(pedido);
    }

    private async Task<Pedido> LocalizarPedido(Guid id)
    {
        var pedido = await _pedidoGateway.ObterPedidoPorId(id);

        return pedido ?? throw new KeyNotFoundException("Pedido não encontrado.");
    }

    public async Task<ConfirmacaoPagamento> PagarPedido(SolicitacaoPagamento solicitacaoPagamento, IPagamentoGateway pagamentoGateway)
    {
        var pedido = await LocalizarPedido(solicitacaoPagamento.PedidoId);

        var pagamentoProcessado = await pagamentoGateway.ProcessarPagamentoAsync(solicitacaoPagamento.Tipo, solicitacaoPagamento.Valor);

        if (pedido.Status != StatusPedido.Pendente)
            throw new Exception($"O status do pedido não permite pagamento.");

        if (pedido.Total != solicitacaoPagamento.Valor)
            throw new Exception($"Valor de pagamento difere do valor do pedido.");

        if (pagamentoProcessado.Status == StatusPagamento.Aprovado)
        {
            pedido.Status = StatusPedido.Recebido;
        }

        pedido.AdicionarPagamento(new PagamentoPedido(solicitacaoPagamento.Tipo, solicitacaoPagamento.Valor, pagamentoProcessado.Status, pagamentoProcessado.Autorizacao));

        await _pedidoGateway.AtualizarPedido(pedido);

        return pagamentoProcessado;
    }
}

using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public class PedidoPresenter
{
    public static PedidoOutputDto Output(Pedido output)
    {
        return new PedidoOutputDto
        {
            Id = output?.Id ?? Guid.Empty,
            TokenAtendimentoId = output?.TokenAtendimentoId ?? Guid.Empty,
            ClienteId = output?.ClienteId ?? Guid.Empty,
            Itens = output?.Itens.Select(i => new ItemPedidoOutputDto
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
                DescontoUnitario = i.DescontoUnitario,
                PrecoUnitario = i.PrecoUnitario
            }).ToList() ?? [],
            Pagamentos = output?.Pagamentos.Select(p => new PagamentoPedidoOutputDto
            {
                Id = p.Id,
                Tipo = p.Tipo.ToString(),
                Status = p.Status.ToString(),
                Troco = p.Troco,
                Autorizacao = p.Autorizacao,
                Valor = p.Valor
            }).ToList() ?? [],
            Subtotal = output?.Subtotal ?? 0,
            Desconto = output?.Desconto ?? 0,
            Total = output?.Total ?? 0,
            Status = output?.Status.ToString() ?? string.Empty
        };
    }
}

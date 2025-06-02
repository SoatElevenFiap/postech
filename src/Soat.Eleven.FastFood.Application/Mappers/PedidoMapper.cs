using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Pedido.Mappers
{
    public static class PedidoMapper
    {
        public static Domain.Entidades.Pedido MapToEntity(PedidoInput dto)
        {
            var pedido = new Domain.Entidades.Pedido(
                dto.TokenAtendimentoId,
                dto.ClienteId,
                dto.Subtotal,
                dto.Desconto,
                dto.Total
            );

            var itens = dto.Itens?.Select(MapToEntity).ToList() ?? [];
            pedido.AdicionarItens(itens);

            return pedido;
        }

        public static ItemPedido MapToEntity(ItemPedidoInput dto)
        {
            return new ItemPedido(
                dto.ProdutoId,
                dto.Quantidade,
                dto.PrecoUnitario,
                dto.DescontoUnitario
            );
        }

        public static PedidoOutput MapToDto(Domain.Entidades.Pedido entity)
        {
            return new PedidoOutput
            {
                Id = entity.Id,
                TokenAtendimentoId = entity.TokenAtendimentoId,
                ClienteId = entity.ClienteId,
                Status = entity.Status.ToString(),
                SenhaPedido = entity.SenhaPedido,
                Subtotal = entity.Subtotal,
                Desconto = entity.Desconto,
                Total = entity.Total,
                Itens = entity.Itens?.Select(MapToDto).ToList() ?? [],
                Pagamentos = entity.Pagamentos?.Select(MapToDto).ToList() ?? []
            };
        }

        public static ItemPedidoOutput MapToDto(ItemPedido entity)
        {
            return new ItemPedidoOutput
            {
                Id = entity.Id,
                ProdutoId = entity.ProdutoId,
                Quantidade = entity.Quantidade,
                PrecoUnitario = entity.PrecoUnitario,
                DescontoUnitario = entity.DescontoUnitario
            };
        }

        public static PagamentoPedidoOutput MapToDto(PagamentoPedido entity)
        {
            return new PagamentoPedidoOutput
            {
                Id = entity.Id,
                Tipo = entity.Tipo.ToString(),
                Valor = entity.Valor,
                Troco = entity.Troco,
                Status = entity.Status.ToString(),
                Autorizacao = entity.Autorizacao
            };
        }
    }
}

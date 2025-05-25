using Soat.Eleven.FastFood.Application.DTOs.Pedidos.Request;
using Soat.Eleven.FastFood.Application.DTOs.Pedidos.Response;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Pedidos.Maps
{
    public static class PedidoMapper
    {
        public static Pedido MapToEntity(PedidoRequestDto dto)
        {
            var pedido = new Pedido(
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

        public static ItemPedido MapToEntity(ItemPedidoRequestDto dto)
        {
            return new ItemPedido(
                dto.ProdutoId,
                dto.Quantidade,
                dto.PrecoUnitario,
                dto.DescontoUnitario
            );
        }

        public static PedidoResponseDto MapToDto(Pedido entity)
        {
            return new PedidoResponseDto
            {
                Id = entity.Id,
                TokenAtendimentoId = entity.TokenAtendimentoId,
                ClienteId = entity.ClienteId,
                Status = entity.Status,
                SenhaPedido = entity.SenhaPedido,
                Subtotal = entity.Subtotal,
                Desconto = entity.Desconto,
                Total = entity.Total,
                Itens = entity.Itens?.Select(MapToDto).ToList() ?? []
            };
        }

        public static ItemPedidoResponseDto MapToDto(ItemPedido entity)
        {
            return new ItemPedidoResponseDto
            {
                Id = entity.Id,
                ProdutoId = entity.ProdutoId,
                Quantidade = entity.Quantidade,
                PrecoUnitario = entity.PrecoUnitario,
                DescontoUnitario = entity.DescontoUnitario
            };
        }
    }
}

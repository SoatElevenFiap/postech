using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Pedidos
{
    public class PedidoOutputDto
    {
        public Guid Id { get; set; }
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public StatusPedido Status { get; set; }
        public string SenhaPedido { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }

        public ICollection<ItemPedidoOutputDto> Itens { get; set; } = [];

        public ICollection<PagamentoPedidoOutputDto> Pagamentos { get; set; } = [];

        /*public static explicit operator PedidoOutputDto(Pedido pedido)
        {
            var pedidoDto = new PedidoOutputDto()
            {
                Id = pedido.Id,
                TokenAtendimentoId = pedido.TokenAtendimentoId,
                ClienteId = pedido.ClienteId,
                Status = pedido.Status.ToString(),
                SenhaPedido = pedido.SenhaPedido,
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total
            };
            
            pedidoDto.Itens = pedido.Itens.Select(i => (ItemPedidoOutputDto)i).ToList();
            pedidoDto.Pagamentos = pedido.Pagamentos.Select(p => (PagamentoPedidoOutputDto)p).ToList();

            return pedidoDto;
        }*/
    }
}

using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Application.DTOs.Pedidos.Response
{
    public class PedidoResponseDto
    {
        public Guid Id { get; set; }
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public StatusPedido Status { get; set; }
        public string SenhaPedido { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }

        public ICollection<ItemPedidoResponseDto> Itens { get; set; } = [];
    }
}

using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Response
{
    public class PedidoResponseDto
    {
        public Guid Id { get; set; }
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public string Status { get; set; }
        public string SenhaPedido { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }

        public ICollection<ItemPedidoResponseDto> Itens { get; set; } = [];

        public ICollection<PagamentoPedidoResponseDto> Pagamentos { get; set; } = [];
    }
}

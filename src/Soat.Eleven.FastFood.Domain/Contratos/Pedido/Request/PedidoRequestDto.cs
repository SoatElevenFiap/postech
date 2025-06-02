namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Request
{
    public class PedidoRequestDto
    {
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemPedidoRequestDto> Itens { get; set; } = [];
    }
}

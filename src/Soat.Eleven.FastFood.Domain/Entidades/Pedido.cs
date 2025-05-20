namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Status { get; set; } = null!;
        public string SenhaPedido { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }

}

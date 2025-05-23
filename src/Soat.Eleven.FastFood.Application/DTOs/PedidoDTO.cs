using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs
{
    public class PedidoDTO
    {
        public Guid? ClienteId { get; set; } // Precisa corrigir no Domain, que está not null
        public string Status { get; set; } = null!;
        public string SenhaPedido { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemPedidoDTO> Itens { get; set; } = new List<ItemPedidoDTO>();
    }
}

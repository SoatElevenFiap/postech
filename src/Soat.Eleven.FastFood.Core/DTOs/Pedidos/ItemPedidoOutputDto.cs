using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Pedidos
{
    public class ItemPedidoOutputDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal DescontoUnitario { get; set; }

        public static explicit operator ItemPedidoOutputDto(ItemPedido itemPedido)
        {
            return new ItemPedidoOutputDto()
            {
                Id = itemPedido.Id,
                ProdutoId = itemPedido.ProdutoId,
                Quantidade = itemPedido.Quantidade,
                PrecoUnitario = itemPedido.PrecoUnitario,
                DescontoUnitario = itemPedido.DescontoUnitario
            };
        }
    }
}

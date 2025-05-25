namespace Soat.Eleven.FastFood.Application.DTOs.Pedido.Request
{
    public class ItemPedidoRequestDto
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal DescontoUnitario { get; set; }
    }
}

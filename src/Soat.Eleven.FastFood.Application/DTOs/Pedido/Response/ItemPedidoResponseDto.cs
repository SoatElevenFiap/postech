namespace Soat.Eleven.FastFood.Application.DTOs.Pedido.Response
{
    public class ItemPedidoResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal DescontoUnitario { get; set; }
    }
}

namespace Soat.Eleven.FastFood.Application.DTOs
{
    public class ItemPedidoDTO
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal? PrecoComDesconto { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}

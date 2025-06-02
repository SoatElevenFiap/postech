namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs
{
    public class ItemPedidoOutput
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal DescontoUnitario { get; set; }
    }
}

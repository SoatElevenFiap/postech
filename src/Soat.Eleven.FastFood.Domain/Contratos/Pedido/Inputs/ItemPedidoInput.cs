namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs
{
    public class ItemPedidoInput
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal DescontoUnitario { get; set; }
    }
}

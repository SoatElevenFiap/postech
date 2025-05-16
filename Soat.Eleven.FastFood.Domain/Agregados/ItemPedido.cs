namespace Soat.Eleven.FastFood.Domain.Agregados
{
    public class ItemPedido
    {
        public string Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Total => Quantidade * PrecoUnitario;
    }
}

namespace Soat.Eleven.FastFood.Domain.Agregados
{
    public class Entrega
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public DateTime DataRetirada { get; private set; }
    }
}

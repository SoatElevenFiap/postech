namespace Soat.Eleven.FastFood.Domain.Agregados
{
    public class Comanda
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public DateTime HorarioPreparacao { get; private set; }
    }
}

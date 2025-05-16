using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Domain.Agregados
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public StatusPagamento Status { get; private set; }
    }
}

using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Pedidos
{
    public class StatusPagamentoPedidoDto
    {
        public StatusPagamento Status { get; set; }
        public Guid PedidoId { get; set; }
    }
}

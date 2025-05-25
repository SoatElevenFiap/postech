using Soat.Eleven.FastFood.Domain.Entidades.Base;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class PagamentoPedido : EntityBase
    {
        public Guid PedidoId { get; set; }
        public TipoPagamento Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Troco { get; set; }
        public StatusPagamento Status { get; set; }
        public string Autorizacao { get; set; }

        public Pedido Pedido { get; set; } = null!;
    }
}

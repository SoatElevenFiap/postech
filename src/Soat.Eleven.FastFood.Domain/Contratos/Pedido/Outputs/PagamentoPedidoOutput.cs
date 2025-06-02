using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs
{
    public class PagamentoPedidoOutput
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Troco { get; set; }
        public string Status { get; set; }
        public string Autorizacao { get; set; }
    }
}

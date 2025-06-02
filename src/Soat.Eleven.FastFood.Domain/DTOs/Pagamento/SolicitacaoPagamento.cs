using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Domain.DTOs.Pagamento
{
    public class SolicitacaoPagamento
    {
        public TipoPagamento Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}

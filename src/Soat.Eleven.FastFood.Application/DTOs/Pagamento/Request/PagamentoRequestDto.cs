using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Application.DTOs.Pagamento.Request
{
    public class PagamentoRequestDto
    {
        public TipoPagamento Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}

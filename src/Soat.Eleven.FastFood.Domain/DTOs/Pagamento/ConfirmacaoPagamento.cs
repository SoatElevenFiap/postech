using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Domain.DTOs.Pagamento
{
    public class ConfirmacaoPagamento
    {
        public ConfirmacaoPagamento(StatusPagamento status, string autorizacao)
        {
            Status = status;
            Autorizacao = autorizacao;
        }

        public StatusPagamento Status { get; set; }
        public string Autorizacao { get; set; }
    }
}

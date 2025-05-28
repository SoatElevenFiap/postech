using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Application.DTOs.Pagamento.Response
{
    public class PagamentoResponseDto
    {
        public PagamentoResponseDto(StatusPagamento status, string autorizacao)
        {
            Status = status;
            Autorizacao = autorizacao;
        }

        public StatusPagamento Status { get; set; }
        public string Autorizacao { get; set; }
    }
}

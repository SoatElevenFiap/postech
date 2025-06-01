using Soat.Eleven.FastFood.Application.DTOs.Pagamento.Response;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPagamentoService
    {
        Task<PagamentoResponseDto> ProcessarPagamento(TipoPagamento Tipo, decimal valor);
    }
}

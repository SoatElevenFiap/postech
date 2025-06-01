using Soat.Eleven.FastFood.Application.DTOs.Pagamento.Response;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class PagamentoService : IPagamentoService
    {
        public Task<PagamentoResponseDto> ProcessarPagamento(TipoPagamento Tipo, decimal valor)
        {
            //Fake checkout
            var response = new PagamentoResponseDto(StatusPagamento.Aprovado, new Random().Next(100000, 999999).ToString());

            return Task.FromResult(response);
        }
    }
}

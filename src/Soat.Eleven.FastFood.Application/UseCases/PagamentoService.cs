using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Application.UseCases
{
    public class PagamentoService : IPagamentoService
    {
        public Task<ConfirmacaoPagamento> ProcessarPagamento(TipoPagamento Tipo, decimal valor)
        {
            //Fake checkout
            var response = new ConfirmacaoPagamento(StatusPagamento.Aprovado, new Random().Next(100000, 999999).ToString());

            return Task.FromResult(response);
        }
    }
}

using Soat.Eleven.FastFood.Domain.UseCases;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public class PagamentoUseCase : IPagamentoUseCase
    {
        private readonly IPagamentoGateway _pagamentoGateway;

        public PagamentoUseCase(IPagamentoGateway pagamentoGateway)
        {
            _pagamentoGateway = pagamentoGateway;
        }

        public Task<ConfirmacaoPagamento> ProcessarPagamento(TipoPagamento Tipo, decimal valor)
        {
            return _pagamentoGateway.ProcessarPagamentoAsync(Tipo, valor);
        }
    }
}

using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

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

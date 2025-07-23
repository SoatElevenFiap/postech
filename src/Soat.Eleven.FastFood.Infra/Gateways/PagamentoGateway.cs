using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class PagamentoGateway : IPagamentoGateway
    {
        public Task<ConfirmacaoPagamento> ProcessarPagamentoAsync(TipoPagamento tipo, decimal valor)
        {
            // Simulação de processamento de pagamento
            // Em um cenário real, aqui seria feita a integração com um provedor de pagamento
            var response = new ConfirmacaoPagamento(
                StatusPagamento.Aprovado, 
                new Random().Next(100000, 999999).ToString()
            );

            return Task.FromResult(response);
        }
    }
}

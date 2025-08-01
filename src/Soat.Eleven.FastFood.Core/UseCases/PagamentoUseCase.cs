using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.DTOs.Webhooks;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public class PagamentoUseCase : IPagamentoUseCase
    {
        private readonly PagamentoGateway _pagamentoGateway;

        public PagamentoUseCase(PagamentoGateway pagamentoGateway)
        {
            _pagamentoGateway = pagamentoGateway;
        }

        public Task<ConfirmacaoPagamento> ProcessarPagamento(NotificacaoPagamentoDto notificacao)
        {
            var confirmacao = _pagamentoGateway.ProcessarPagamento(Guid.Parse(notificacao.ExternalId));
            return confirmacao;
        }

        public async Task<ConfirmacaoPagamento> ConfirmarPagamento(NotificacaoPagamentoDto notificacao)
        {
            var confirmacao = await _pagamentoGateway.AprovarPagamento(Guid.Parse(notificacao.ExternalId));
            return confirmacao;
        }
    }
}

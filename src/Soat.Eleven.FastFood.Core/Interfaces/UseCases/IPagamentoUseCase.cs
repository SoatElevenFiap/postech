using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.DTOs.Webhooks;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IPagamentoUseCase
{
    Task<ConfirmacaoPagamento> ProcessarPagamento(NotificacaoPagamentoDto notificacao);
    
    Task<ConfirmacaoPagamento> ConfirmarPagamento(NotificacaoPagamentoDto notificacao);
}

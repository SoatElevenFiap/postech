using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IPagamentoGateway
{
    Task<ConfirmacaoPagamento> ProcessarPagamentoAsync(TipoPagamento tipo, decimal valor);
}

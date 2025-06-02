using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPagamentoService
    {
        Task<ConfirmacaoPagamento> ProcessarPagamento(TipoPagamento Tipo, decimal valor);
    }
}

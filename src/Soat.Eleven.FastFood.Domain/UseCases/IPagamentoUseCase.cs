using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;

namespace Soat.Eleven.FastFood.Domain.UseCases
{
    public interface IPagamentoUseCase
    {
        Task<ConfirmacaoPagamento> ProcessarPagamento(TipoPagamento tipo, decimal valor);
    }
}

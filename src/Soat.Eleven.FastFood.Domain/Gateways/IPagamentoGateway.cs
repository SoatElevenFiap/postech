using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface IPagamentoGateway
    {
        Task<ConfirmacaoPagamento> ProcessarPagamentoAsync(TipoPagamento tipo, decimal valor);
    }
}

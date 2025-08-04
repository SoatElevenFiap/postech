using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;

namespace Soat.Eleven.FastFood.Common.Interfaces.DataSources;

public interface IPagamentoDataSource
{
    Task UpdateAsync(Guid pedidoId, ConfirmacaoPagamento produto);
}
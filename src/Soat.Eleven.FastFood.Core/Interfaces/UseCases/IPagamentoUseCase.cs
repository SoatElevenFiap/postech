using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IPagamentoUseCase
{
    Task<ConfirmacaoPagamento> ProcessarPagamento(TipoPagamento tipoPagamento, decimal value);
}

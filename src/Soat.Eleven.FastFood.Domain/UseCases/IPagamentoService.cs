using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IPagamentoService<Pediddo> where Pediddo : IEntity
    {
        Task<Pediddo> ProcessarPagamento(TipoPagamento Tipo, decimal valor);
    }
}

using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Gateways;

public class PagamentoGateway
{
    private IPagamentoDataSource _pagamentoDataSource;
    
    public PagamentoGateway(IPagamentoDataSource pagamentoDataSource)
    {
        _pagamentoDataSource = pagamentoDataSource;
    }
    
    public async Task<ConfirmacaoPagamento> ProcessarPagamento(Guid pedidoId)
    {
        ConfirmacaoPagamento confirmacaoPagamento = new ConfirmacaoPagamento(
            StatusPagamento.Pendente, 
            new Random().Next(100000, 999999).ToString());
        await _pagamentoDataSource.UpdateAsync(pedidoId, confirmacaoPagamento);
        return confirmacaoPagamento;
    }
    
    public async Task<ConfirmacaoPagamento> AprovarPagamento(Guid pedidoId)
    {
        ConfirmacaoPagamento confirmacaoPagamento = new ConfirmacaoPagamento(
            StatusPagamento.Aprovado, 
            new Random().Next(100000, 999999).ToString());
        await _pagamentoDataSource.UpdateAsync(pedidoId, confirmacaoPagamento);
        return confirmacaoPagamento;
    }
}
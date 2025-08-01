using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;

namespace Soat.Eleven.FastFood.Core.Gateways
{
    public class PedidoGateway
    {
        private IPedidoDataSource _pedidoDataSource;

        public PedidoGateway(IPedidoDataSource dataSource)
        {
            _pedidoDataSource = dataSource;
        }

        public async Task<Pedido> AtualizarPedido(Pedido pedido)
        {
            await _pedidoDataSource.UpdateAsync(pedido);
            return pedido;
        }

        public async Task<Pedido> CriarPedido(Pedido pedido)
        {
            var entity = await _pedidoDataSource.AddAsync(pedido);

            return entity;
        }

        public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            return await _pedidoDataSource.GetAllAsync();
        }

        public async Task<Pedido?> ObterPedidoPorId(Guid id)
        {
            return await _pedidoDataSource.GetByIdAsync(id);
        }

        public Task<ConfirmacaoPagamento> PagarPedido(SolicitacaoPagamento solicitacaoPagamento, PagamentoGateway pagamentoGateway)
        {
            throw new NotImplementedException();
        }
    }
}

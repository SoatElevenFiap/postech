using Soat.Eleven.FastFood.Application.DTOs.Pedido.Mappers;
using Soat.Eleven.FastFood.Domain.UseCases;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Outputs;
using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPagamentoUseCase _pagamentoUseCase;

        public PedidoUseCase(IPedidoGateway pedidoGateway, IPagamentoUseCase pagamentoUseCase)
        {
            _pedidoGateway = pedidoGateway;
            _pagamentoUseCase = pagamentoUseCase;
        }

        public async Task<PedidoOutput> CriarPedido(PedidoInput pedidoDto)
        {
            var pedido = PedidoMapper.MapToEntity(pedidoDto);

            pedido.GerarSenha();

            pedido = await _pedidoGateway.AddAsync(pedido);

            var pedidoResponse = PedidoMapper.MapToDto(pedido);

            return pedidoResponse;
        }

        public async Task<PedidoOutput> AtualizarPedido(Guid id, PedidoInput pedidoDto)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.Pendente)
                throw new Exception($"O status do pedido não permite alteração.");

            pedido.TokenAtendimentoId = pedidoDto.TokenAtendimentoId;
            pedido.ClienteId = pedidoDto.ClienteId;
            pedido.Subtotal = pedidoDto.Subtotal;
            pedido.Desconto = pedidoDto.Desconto;
            pedido.Total = pedidoDto.Total;
            pedido.ModificadoEm = DateTime.Now;

            pedido.Itens.Clear();

            var novosItens = pedidoDto.Itens?.Select(PedidoMapper.MapToEntity).ToList() ?? [];
            pedido.AdicionarItens(novosItens);

            await _pedidoGateway.UpdateAsync(pedido);

            return PedidoMapper.MapToDto(pedido);
        }

        public async Task<IEnumerable<PedidoOutput>> ListarPedidos()
        {
            var pedidos = await _pedidoGateway.GetAllAsync();

            var pedidosDto = pedidos.Select(p => PedidoMapper.MapToDto(p));

            return pedidosDto;
        }

        public async Task<PedidoOutput?> ObterPedidoPorId(Guid id)
        {
            var pedido = await _pedidoGateway.GetByIdAsync(id);

            if (pedido == null)
                return null;

            return PedidoMapper.MapToDto(pedido);
        }

        public async Task IniciarPreparacaoPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.Recebido)
                throw new Exception($"O status do pedido não permite iniciar a preparação. Status atual: {pedido.Status} ");

            pedido.Status = StatusPedido.EmPreparacao;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoGateway.UpdateAsync(pedido);
        }

        public async Task FinalizarPreparacaoPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.EmPreparacao)
                throw new Exception($"O status do pedido não está permite finalizar a preparação. Status atual: {pedido.Status} ");

            pedido.Status = StatusPedido.Pronto;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoGateway.UpdateAsync(pedido);
        }

        public async Task FinalizarPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.Pronto)
                throw new Exception($"O status do pedido não permite finalização. Status atual: {pedido.Status} ");

            pedido.Status = StatusPedido.Finalizado;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoGateway.UpdateAsync(pedido);
        }

        public async Task CancelarPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status == StatusPedido.Finalizado)
                throw new Exception($"Não é permitido cancelar pedido finalizado");

            pedido.Status = StatusPedido.Cancelado;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoGateway.UpdateAsync(pedido);
        }

        private async Task<Pedido> LocalizarPedido(Guid id)
        {
            var pedido = await _pedidoGateway.GetByIdAsync(id);

            return pedido ?? throw new Exception("Pedido não encontrado.");
        }

        public async Task<ConfirmacaoPagamento> PagarPedido(Guid id, SolicitacaoPagamento pagamento)
        {
            var pedido = await LocalizarPedido(id);

            var pagamentoProcessado = await _pagamentoUseCase.ProcessarPagamento(pagamento.Tipo, pagamento.Valor);

            if (pedido.Status != StatusPedido.Pendente)
                throw new Exception($"O status do pedido não permite pagamento.");

            if (pedido.Total != pagamento.Valor)
                throw new Exception($"Valor de pagamento difere do valor do pedido.");

            if (pagamentoProcessado.Status == StatusPagamento.Aprovado)
            {
                pedido.Status = StatusPedido.Recebido;
            }

            pedido.ModificadoEm = DateTime.Now;
            pedido.AdicionarPagamento(new PagamentoPedido(pagamento.Tipo, pagamento.Valor, pagamentoProcessado.Status, pagamentoProcessado.Autorizacao));

            await _pedidoGateway.UpdateAsync(pedido);

            return pagamentoProcessado;
        }
    }
}

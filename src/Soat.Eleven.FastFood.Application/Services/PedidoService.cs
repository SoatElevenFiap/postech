using Soat.Eleven.FastFood.Application.DTOs.Pedido.Mappers;
using Soat.Eleven.FastFood.Application.DTOs.Pedido.Request;
using Soat.Eleven.FastFood.Application.DTOs.Pedido.Response;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IRepository<Pedido> _pedidoRepository;

        public PedidoService(IRepository<Pedido> pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoResponseDto> CriarPedido(PedidoRequestDto pedidoDto)
        {
            var pedido = PedidoMapper.MapToEntity(pedidoDto);            

            pedido.GerarSenha();

            pedido = await _pedidoRepository.AddAsync(pedido);

            var pedidoResponse = PedidoMapper.MapToDto(pedido);

            return pedidoResponse;
        }

        public async Task<PedidoResponseDto> AtualizarPedido(Guid id, PedidoRequestDto pedidoDto)
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

            var itens = pedidoDto.Itens?.Select(PedidoMapper.MapToEntity).ToList() ?? [];
            pedido.Itens.Clear();
            pedido.AdicionarItens(itens);

            await _pedidoRepository.UpdateAsync(pedido);

            return PedidoMapper.MapToDto(pedido);
        }

        public async Task<IEnumerable<PedidoResponseDto>> ListarPedidos()
        {
            var pedidos = await _pedidoRepository.GetAllAsync(e => e.Itens);

            var pedidosDto = pedidos.Select(p => PedidoMapper.MapToDto(p));

            return pedidosDto;
        }

        public async Task<PedidoResponseDto?> ObterPedidoPorId(Guid id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

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

            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task FinalizarPreparacaoPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.EmPreparacao)
                throw new Exception($"O status do pedido não está permite finalizar a preparação. Status atual: {pedido.Status} ");

            pedido.Status = StatusPedido.Pronto;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task FinalizarPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            if (pedido.Status != StatusPedido.Pronto)
                throw new Exception($"O status do pedido não permite finalização. Status atual: {pedido.Status} ");

            pedido.Status = StatusPedido.Finalizado;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task CancelarPedido(Guid id)
        {
            var pedido = await LocalizarPedido(id);

            pedido.Status = StatusPedido.Finalizado;
            pedido.ModificadoEm = DateTime.Now;

            await _pedidoRepository.UpdateAsync(pedido);
        }       

        private async Task<Pedido> LocalizarPedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);

            return pedido ?? throw new Exception("Pedido não encontrado.");
        }
    }
}

using Soat.Eleven.FastFood.Application.DTOs;
using Soat.Eleven.FastFood.Application.Services.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
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

        public async Task<PedidoDTO> CriarPedido(PedidoDTO pedido)
        {
            var pedidoId = Guid.NewGuid();

            var pedidoEntity = new Pedido
            {
                Id = pedidoId,
                ClienteId = pedido.ClienteId ?? throw new ArgumentNullException(nameof(pedido.ClienteId)),
                Status = pedido.Status,
                SenhaPedido = pedido.SenhaPedido,
                DataCriacao = pedido.DataCriacao,
                Subtotal = pedido.Subtotal,
                Desconto = pedido.Desconto,
                Total = pedido.Total,
                Itens = pedido.Itens.Select(x => new ItemPedido
                {
                    Id = Guid.NewGuid(),
                    PedidoId = pedidoId,
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade,
                    PrecoUnitario = x.PrecoUnitario,
                    PrecoComDesconto = x.PrecoComDesconto
                }).ToList()
            };

            var novoPedido = await _pedidoRepository.AddAsync(pedidoEntity);

            return pedido;
        }

        public async Task<List<PedidoDTO>> ListarPedido()
        {
            var pedidos = await _pedidoRepository.GetAllAsync(e => e.Itens);

            var pedidosDto = pedidos.Select(p => new PedidoDTO
            {
                ClienteId = p.ClienteId,
                Status = p.Status,
                SenhaPedido = p.SenhaPedido,
                DataCriacao = p.DataCriacao,
                Subtotal = p.Subtotal,
                Desconto = p.Desconto,
                Total = p.Total,
                Itens = p.Itens.Select(i => new ItemPedidoDTO
                {
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    PrecoComDesconto = i.PrecoComDesconto
                }).ToList()
            });

            return pedidosDto.ToList();
        }
    }
}

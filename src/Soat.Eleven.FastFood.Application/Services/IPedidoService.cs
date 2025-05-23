using Soat.Eleven.FastFood.Application.DTOs;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Services
{
    public interface IPedidoService
    {
        Task<PedidoDTO> CriarPedido(PedidoDTO pedido);
        Task<List<PedidoDTO>> ListarPedido();
    }
}

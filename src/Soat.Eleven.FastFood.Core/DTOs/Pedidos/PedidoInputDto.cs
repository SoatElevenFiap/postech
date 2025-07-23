using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Pedidos
{
    public class PedidoInputDto
    {
        public Guid Id { get; set; }
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemPedidoInputDto> Itens { get; set; } = [];

        public static implicit operator Pedido(PedidoInputDto inputDto)
        {
            var pedido = new Pedido(inputDto.TokenAtendimentoId, inputDto.ClienteId, inputDto.Subtotal, inputDto.Desconto, inputDto.Total);

            pedido.Itens = inputDto.Itens.Select(i =>
            {
                return new ItemPedido(i.ProdutoId, i.Quantidade, i.PrecoUnitario, i.DescontoUnitario);
            }).ToList();

            return pedido;
        }
    }
}

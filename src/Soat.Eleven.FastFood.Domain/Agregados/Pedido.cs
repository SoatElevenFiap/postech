using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Domain.Agregados
{

    public class Pedido
    {
        public Guid Id { get; private set; }
        public Cliente Cliente { get; private set; }
        public List<ItemPedido> Itens { get; private set; }
        public StatusPedido Status { get; private set; }
        public void AdicionarItem(ItemPedido item) { /*...*/ }
        public void FinalizarPedido() { /*...*/ }
    }
}

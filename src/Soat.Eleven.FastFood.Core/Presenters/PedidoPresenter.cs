using Soat.Eleven.FastFood.Core.DTOs.Pedidos;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public class PedidoPresenter
{
    public static Pedido Input(object? input)
    {
        try
        {
            return (Pedido)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static PedidoOutputDto Output(Pedido? output)
    {
        try
        {
            return (PedidoOutputDto)output!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

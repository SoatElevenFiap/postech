using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public static class UsuarioPresenter
{
    public static Cliente Input(CriarClienteRequestDto? input)
    {
        try
        {
            return (Cliente)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Cliente Input(AtualizarClienteRequestDto? input)
    {
        try
        {
            return (Cliente)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

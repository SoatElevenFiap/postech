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

    public static Administrador Input(CriarAdmRequestDto? input)
    {
        try
        {
            return (Administrador)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Administrador Input(AtualizarAdmRequestDto? input)
    {
        try
        {
            return (Administrador)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static UsuarioAdmResponseDto Output(Administrador output)
    {
        try
        {
            return (UsuarioAdmResponseDto)output;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static UsuarioClienteResponseDto Output(Cliente output)
    {
        try
        {
            return (UsuarioClienteResponseDto)output;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public static class UsuarioPresenter
{
    public static Usuario Input(CriarAdmRequestDto input)
    {
        return new Usuario
        {
            Nome = input.Nome,
            Email = input.Email,
            Telefone = input.Telefone,
            Senha = input.Senha
        };
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

    public static Usuario Input(AtualizarAdmRequestDto request)
    {
        return new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone
        };
    }

    public static Cliente Input(CriarClienteRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public static UsuarioAdmResponseDto Output(Usuario result)
    {
        return new UsuarioAdmResponseDto
        {
            Id = result.Id,
            Nome = result.Nome,
            Email = result.Email,
            Telefone = result.Telefone
        };
    }

    public static UsuarioClienteResponseDto Output(Cliente result)
    {
        return new UsuarioClienteResponseDto
        {
            Id = result.Id,
            Nome = result.Nome,
            Email = result.Email,
            Telefone = result.Telefone
        };
    }
}

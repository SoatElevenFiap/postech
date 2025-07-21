using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

public class AtualizarAdmRequestDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public static implicit operator Usuario(AtualizarAdmRequestDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email, dto.Telefone, PerfilUsuario.Administrador);

        return usuario;
    }
}

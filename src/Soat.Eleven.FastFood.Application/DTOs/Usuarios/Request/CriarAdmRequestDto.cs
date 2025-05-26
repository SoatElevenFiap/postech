using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

public class CriarAdmRequestDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }

    public static implicit operator Usuario(CriarAdmRequestDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email, dto.Senha, dto.Telefone, Domain.Enums.PerfilUsuario.Administrador);

        return usuario;
    }
}

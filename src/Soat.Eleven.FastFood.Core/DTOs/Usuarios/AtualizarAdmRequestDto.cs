using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Usuarios;

public class AtualizarAdmRequestDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public static implicit operator Administrador(AtualizarAdmRequestDto dto)
    {
        var usuario = new Administrador()
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Perfil = PerfilUsuario.Administrador
        };

        return usuario;
    }
}

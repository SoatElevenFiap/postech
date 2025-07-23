using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Usuarios;

public class CriarAdmRequestDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }

    public static implicit operator Administrador(CriarAdmRequestDto dto)
    {
        var usuario = new Administrador(dto.Nome,
                                        dto.Email,
                                        dto.Senha,
                                        dto.Telefone,
                                        PerfilUsuario.Administrador,
                                        StatusUsuario.Ativo);

        return usuario;
    }
}

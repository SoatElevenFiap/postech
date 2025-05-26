using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;

public class UsuarioAdmResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public static explicit operator UsuarioAdmResponseDto(Usuario usuario)
    {
        return new UsuarioAdmResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone
        };
    }
}

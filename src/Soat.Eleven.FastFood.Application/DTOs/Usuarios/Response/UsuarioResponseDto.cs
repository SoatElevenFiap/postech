using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;

public class UsuarioResponseDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public static explicit operator UsuarioResponseDto(Usuario usuario)
    {
        var dto = new UsuarioResponseDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone
        };

        if (usuario.Cliente is not null)
        {
            dto.Cpf = usuario.Cliente.Cpf;
            dto.DataDeNascimento = usuario.Cliente.DataDeNascimento;
        }

        return dto;
    }
}

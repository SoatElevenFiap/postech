using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;

public class UsuarioClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static explicit operator UsuarioClienteResponseDto(Usuario usuario)
    {
        return new UsuarioClienteResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Cpf = usuario.Cliente.Cpf,
            DataDeNascimento = usuario.Cliente.DataDeNascimento
        };
    }
}

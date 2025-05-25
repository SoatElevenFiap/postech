using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

public class CriarClienteRequestDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static implicit operator Usuario(CriarClienteRequestDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email, dto.Senha, dto.Telefone, Domain.Enums.PerfilUsuario.Cliente);
        usuario.CriarCliente(dto.Cpf, dto.DataDeNascimento);

        return usuario;
    }
}

using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Usuarios;

public class AtualizarClienteRequestDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }

    public static implicit operator Cliente(AtualizarClienteRequestDto dto)
    {
        var usuario = new Cliente()
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Cpf = dto.Cpf,
            DataDeNascimento = dto.DataDeNascimento
        };

        return usuario;
    }
}

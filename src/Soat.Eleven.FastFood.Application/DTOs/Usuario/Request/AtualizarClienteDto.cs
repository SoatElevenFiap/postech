using Microsoft.AspNetCore.Mvc;

namespace Soat.Eleven.FastFood.Application.DTOs.Usuario.Request;

public class AtualizarClienteDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
}

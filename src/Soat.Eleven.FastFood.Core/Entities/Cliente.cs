using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.ValueObjects;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Cliente : Usuario
{
    public Cliente(string nome,
                   string email,
                   Password senha,
                   string telefone,
                   PerfilUsuario perfil,
                   StatusUsuario status,
                   DocumentCPF cpf,
                   DateTime dataDeNascimento) : base(nome, email, senha, telefone, perfil, status)
    {
        Cpf = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public Cliente()
    {
    }

    public DocumentCPF Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
}

using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Cliente : Usuario
{

    public Cliente(string nome,
                   string email,
                   string senha,
                   string telefone,
                   PerfilUsuario perfil,
                   StatusUsuario status,
                   string cpf,
                   DateTime dataDeNascimento) : base(nome, email, senha, telefone, perfil, status)
    {
        Cpf = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public Cliente()
    {
    }

    public Cliente(Guid id,
                   string nome,
                   string email,
                   string senha,
                   string telefone,
                   PerfilUsuario perfil,
                   StatusUsuario status,
                   string cpf,
                   DateTime dataDeNascimento) : base(id, nome, email, senha, telefone, perfil, status)
    {
        Cpf = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public string Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
}

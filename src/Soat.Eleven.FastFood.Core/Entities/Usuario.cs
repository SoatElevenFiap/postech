using Soat.Eleven.FastFood.Core.ConditionRules;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.ValueObjects;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    private string nome;

    public string Nome
    {
        get { return nome; }
        set {
            Condition.Require(value, "Nome").IsNullOrEmpty();
            nome = value; 
        }
    }

    private string email;

    public string Email
    {
        get { return email; }
        set {
            Condition.Require(value, "Email").IsEmail();
            email = value; 
        }
    }

    public Password Senha { get; set; }
    public string Telefone { get; set; }
    public PerfilUsuario Perfil { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
    public StatusUsuario Status { get; set; }
}

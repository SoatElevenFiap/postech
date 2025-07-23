using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.ValueObjects;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Administrador : Usuario
{
    public Administrador(string nome,
                         string email,
                         Password senha,
                         string telefone,
                         PerfilUsuario perfil,
                         StatusUsuario status) : base(nome, email, senha, telefone, perfil, status)
    {
    }

    public Administrador()
    {
    }
}

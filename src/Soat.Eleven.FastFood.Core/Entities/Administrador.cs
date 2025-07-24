using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Administrador : Usuario
{

    public Administrador(string nome,
                         string email,
                         string senha,
                         string telefone,
                         PerfilUsuario perfil,
                         StatusUsuario status) : base(nome, email, senha, telefone, perfil, status)
    {
    }

    public Administrador()
    {
    }

    public Administrador(Guid id,
                         string nome,
                         string email,
                         string senha,
                         string telefone,
                         PerfilUsuario perfil,
                         StatusUsuario status) : base(id, nome, email, senha, telefone, perfil, status)
    {
    }
}

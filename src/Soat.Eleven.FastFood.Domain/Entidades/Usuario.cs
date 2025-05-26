using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Usuario : IEntity, IAuditable
    {
        public Usuario(string nome, string email, string senha, string telefone, PerfilUsuario perfil)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Telefone = telefone;
            Perfil = perfil;
        }

        public Usuario(string nome, string email, string telefone, PerfilUsuario perfil)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Perfil = perfil;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
        public StatusUsuario Status { get; set; }
        public Cliente Cliente { get; set; }

        public void CriarCliente(string cpf, DateTime dataNascimento)
        {
            Cliente = new Cliente(cpf, dataNascimento);
        }
    }
}

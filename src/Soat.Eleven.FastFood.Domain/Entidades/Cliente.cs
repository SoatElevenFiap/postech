using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Cliente : IEntity, IAuditable
    {
        public Cliente(string cpf, DateTime dataDeNascimento)
        {
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
        }

        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }

}

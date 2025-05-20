namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class UsuarioSistema
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public int PerfilId { get; set; }
        public DateTime CriadoEm { get; set; }
        public Perfil Perfil { get; set; } = null!;
    }

}

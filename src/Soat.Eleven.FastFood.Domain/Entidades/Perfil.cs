namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<UsuarioSistema> Usuarios { get; set; } = new List<UsuarioSistema>();
    }

}

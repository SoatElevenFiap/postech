namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Comanda
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CriadaEm { get; set; }
        public Cliente Cliente { get; set; } = null!;
    }

}

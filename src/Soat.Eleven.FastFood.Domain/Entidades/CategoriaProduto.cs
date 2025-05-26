namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class CategoriaProduto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; } = true;

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }

}

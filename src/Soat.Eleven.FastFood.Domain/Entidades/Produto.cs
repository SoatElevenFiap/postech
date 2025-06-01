namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string SKU { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public Guid CategoriaId { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
        public string? Imagem { get; set; }
        public CategoriaProduto Categoria { get; set; } = null!;
    }

}

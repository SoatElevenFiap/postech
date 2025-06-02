namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Produto
{
    public class ProdutoDTO
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
    }
}
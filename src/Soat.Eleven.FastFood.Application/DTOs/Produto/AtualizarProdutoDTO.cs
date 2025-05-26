namespace Soat.Eleven.FastFood.Application.DTOs.Produto
{
    public class AtualizarProdutoDTO
    {
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
    }
} 
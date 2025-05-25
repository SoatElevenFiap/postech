namespace Soat.Eleven.FastFood.Application.DTOs.Categoria
{
    public class CategoriaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
    }
} 
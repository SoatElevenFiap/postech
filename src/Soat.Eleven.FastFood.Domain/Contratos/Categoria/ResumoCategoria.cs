namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Categoria
{
    public class ResumoCategoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
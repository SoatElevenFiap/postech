using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Categorias
{
    public class AtualizarCategoriaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }

        public static implicit operator CategoriaProduto(AtualizarCategoriaDto atualizarCategoria)
        {
            return new CategoriaProduto()
            {
                Id = atualizarCategoria.Id,
                Nome = atualizarCategoria.Nome,
                Descricao = atualizarCategoria.Descricao,
                Ativo = atualizarCategoria.Ativo
            };
        }
    }
}

using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Categorias
{
    public class CriarCategoriaDto
    {
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }

        public static implicit operator CategoriaProduto(CriarCategoriaDto criarCategoria)
        {
            return new CategoriaProduto()
            {
                Nome = criarCategoria.Nome,
                Descricao = criarCategoria.Descricao,
            };
        }
    }
}

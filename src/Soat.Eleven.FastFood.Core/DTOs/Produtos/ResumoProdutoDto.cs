using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.DTOs.Produtos;

public class ResumoProdutoDto
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

    public static implicit operator ResumoProdutoDto(Produto entity)
    {
        var produto = new ResumoProdutoDto()
        {
            Id = entity.Id,
            Nome = entity.Nome,
            SKU = entity.SKU,
            Descricao = entity.Descricao,
            Preco = entity.Preco,
            CategoriaId = entity.CategoriaId,
            Ativo = entity.Ativo,
            Imagem = entity.Imagem
        };

        return produto;
    }
}
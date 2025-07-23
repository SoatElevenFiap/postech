using Soat.Eleven.FastFood.Core.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soat.Eleven.FastFood.Core.DTOs.Produtos
{
    public class AtualizarProdutoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public string? Imagem { get; set; }


        [JsonExtensionData]
        public Dictionary<string, JsonElement>? CamposExtras { get; set; }

        public bool ImagemFoiEnviada()
        {
            return CamposExtras?.ContainsKey(nameof(Imagem)) == true;
        }

        public static implicit operator Produto(AtualizarProdutoDto dto)
        {
            var produto = new Produto()
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco ?? 0,
                Imagem = dto.Imagem
            };

            return produto;
        }
    }
}
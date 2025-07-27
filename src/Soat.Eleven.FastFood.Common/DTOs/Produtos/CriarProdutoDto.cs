using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soat.Eleven.FastFood.Common.DTOs.Produtos
{
    public class CriarProdutoDto
    {
        public string Nome { get; set; } = null!;
        public string SKU { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public Guid CategoriaId { get; set; }


        [JsonExtensionData]
        public Dictionary<string, JsonElement>? CamposExtras { get; set; }

        public bool ImagemFoiEnviada()
        {
            return CamposExtras?.ContainsKey(nameof(Imagem)) == true;
        }      
    }
}
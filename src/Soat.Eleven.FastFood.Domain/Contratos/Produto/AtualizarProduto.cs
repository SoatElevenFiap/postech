using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Produto
{
    public class AtualizarProduto
    {
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
    }
}
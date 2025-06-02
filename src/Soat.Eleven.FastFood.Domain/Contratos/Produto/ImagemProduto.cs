namespace Soat.Eleven.FastFood.Core.Domain.Contratos.Produto
{
    public class ImagemProduto
    {
        public string Nome { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public Stream Conteudo { get; set; } = null!;
    }
}
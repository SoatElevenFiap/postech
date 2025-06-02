namespace Soat.Eleven.FastFood.Application.DTOs.Produto
{
    public class ImagemUploadDTO
    {
        public string Nome { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public Stream Conteudo { get; set; } = null!;
    }
} 
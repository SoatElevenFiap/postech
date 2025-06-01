namespace Soat.Eleven.FastFood.Application.DTOs.Common
{
    public class ArquivoUploadDTO
    {
        public string Nome { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public Stream Conteudo { get; set; } = null!;
    }
} 
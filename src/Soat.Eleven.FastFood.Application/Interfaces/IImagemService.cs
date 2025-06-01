using Soat.Eleven.FastFood.Application.DTOs.Common;

namespace Soat.Eleven.FastFood.Application.Interfaces
{
    public interface IImagemService
    {
        Task<string> UploadImagemAsync(string diretorio, string identificador, ArquivoUploadDTO imagem);
        Task RemoverImagemAsync(string diretorio, string identificador);
        Task<string> ObterUrlImagemAsync(string diretorio, string nomeArquivo);
        string GerarNomeArquivo(string base64);
        string? ObterExtensaoBase64(string? base64);
    }
} 
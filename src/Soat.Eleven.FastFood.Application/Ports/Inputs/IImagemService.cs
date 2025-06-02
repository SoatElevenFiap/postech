using Soat.Eleven.FastFood.Application.DTOs.Common;
using Soat.Eleven.FastFood.Application.DTOs.Produto;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IImagemService
    {
        Task<string> UploadImagemAsync(string diretorio, string identificador, ImagemUploadDTO imagem);
        Task RemoverImagemAsync(string diretorio, string identificador);
        Task<string> ObterUrlImagemAsync(string diretorio, string nomeArquivo);
        string GerarNomeArquivo(string base64);
        string? ObterExtensaoBase64(string? base64);
    }
}
namespace Soat.Eleven.FastFood.Core.Interfaces.Services;

public interface IImagemService
{
    //Task<string> UploadImagemAsync(string diretorio, string identificador, ImagemProdutoArquivo imagem);
    Task RemoverImagemAsync(string diretorio, string identificador);
    Task<string> ObterUrlImagemAsync(string diretorio, string nomeArquivo);
    string GerarNomeArquivo(string base64);
    string? ObterExtensaoBase64(string? base64);
}

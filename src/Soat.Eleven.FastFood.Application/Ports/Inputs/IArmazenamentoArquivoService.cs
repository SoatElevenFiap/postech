using Soat.Eleven.FastFood.Application.DTOs.Common;
using Soat.Eleven.FastFood.Application.DTOs.Produto;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IArmazenamentoArquivoService
    {
        Task<string> SalvarArquivoAsync(string diretorio, string identificador, ImagemUploadDTO arquivo);
        Task RemoverArquivoAsync(string diretorio, string identificador);
        Task<string> ObterUrlArquivoAsync(string diretorio, string nomeArquivo);
    }
}
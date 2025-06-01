using Soat.Eleven.FastFood.Application.DTOs.Common;

namespace Soat.Eleven.FastFood.Application.Interfaces
{
    public interface IArmazenamentoArquivoService
    {
        Task<string> SalvarArquivoAsync(string diretorio, string identificador, ArquivoUploadDTO arquivo);
        Task RemoverArquivoAsync(string diretorio, string identificador);
        Task<string> ObterUrlArquivoAsync(string diretorio, string nomeArquivo);
    }
} 
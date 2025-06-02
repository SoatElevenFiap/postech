using Soat.Eleven.FastFood.Core.Domain.Contratos.Produto;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IArmazenamentoArquivoService
    {
        Task<string> SalvarArquivoAsync(string diretorio, string identificador, ImagemProdutoArquivo arquivo);
        Task RemoverArquivoAsync(string diretorio, string identificador);
        Task<string> ObterUrlArquivoAsync(string diretorio, string nomeArquivo);
    }
}
using Soat.Eleven.FastFood.Core.Domain.Contratos.Produto;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IProdutoService
    {
        Task<IEnumerable<ResumoProduto>> ListarProdutos(bool? incluirInativos = false, Guid? categoryId = null);
        Task<ResumoProduto?> ObterProdutoPorId(Guid id);
        Task<ResumoProduto> CriarProduto(ResumoProduto produto);
        Task<ResumoProduto> AtualizarProduto(Guid id, AtualizarProduto produto);
        Task DesativarProduto(Guid id);
        Task ReativarProduto(Guid id);
        Task<string> UploadImagemAsync(Guid produtoId, ImagemProdutoArquivo imagem);
        Task RemoverImagemAsync(Guid produtoId);
    }
}
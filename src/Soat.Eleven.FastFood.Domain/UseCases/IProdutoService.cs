using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs
{
    public interface IProdutoService<Produto> where Produto : IEntity
    {
        Task<IEnumerable<Produto>> ListarProdutos(bool? incluirInativos = false, Guid? categoryId = null);
        Task<Produto?> ObterProdutoPorId(Guid id);
        Task<Produto> CriarProduto(Produto produto);
        Task<Produto> AtualizarProduto(Guid id, Produto produto);
        Task DesativarProduto(Guid id);
        Task ReativarProduto(Guid id);
        Task<string> UploadImagemAsync(Guid produtoId, Produto imagem);
        Task RemoverImagemAsync(Guid produtoId);
    }
}
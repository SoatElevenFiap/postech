using Soat.Eleven.FastFood.Application.DTOs.Produto;

namespace Soat.Eleven.FastFood.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> ListarProdutos(bool? incluirInativos = false, Guid? categoryId = null);
        Task<ProdutoDTO?> ObterProdutoPorId(Guid id);
        Task<ProdutoDTO> CriarProduto(ProdutoDTO produto);
        Task<ProdutoDTO> AtualizarProduto(Guid id, AtualizarProdutoDTO produto);
        Task DesativarProduto(Guid id);
        Task ReativarProduto(Guid id);
        Task<string> UploadImagemAsync(Guid produtoId, ImagemUploadDTO imagem);
        Task RemoverImagemAsync(Guid produtoId);
    }
} 
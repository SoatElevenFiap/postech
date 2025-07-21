using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IProdutoUseCase
{
    Task<IEnumerable<Produto>> ListarProdutos(ICategoriaGateway categoriaGateway, bool? incluirInativos = false, Guid? categoryId = null);
    Task<Produto?> ObterProdutoPorId(Guid id);
    Task<Produto> CriarProduto(Produto produto, ICategoriaGateway categoriaGateway);
    Task<Produto> AtualizarProduto(Guid id, Produto produto);
    Task DesativarProduto(Guid id);
    Task ReativarProduto(Guid id);
    //Task<string> UploadImagemAsync(Guid produtoId, ImagemProdutoArquivo imagem);
    //Task RemoverImagemAsync(Guid produtoId);
}
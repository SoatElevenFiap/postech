using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.DTOs.Images;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IProdutoUseCase
{
    Task<IEnumerable<Produto>> ListarProdutos(ICategoriaProdutoDataSource categoriaGateway, bool? incluirInativos = false, Guid? categoryId = null);
    Task<Produto?> ObterProdutoPorId(Guid id);
    Task<Produto> CriarProduto(Produto produto, ICategoriaProdutoDataSource categoriaGateway);
    Task<Produto> AtualizarProduto(Produto produto);
    Task DesativarProduto(Guid id);
    Task ReativarProduto(Guid id);
    Task<string> UploadImagemAsync(Guid produtoId, ImagemProdutoArquivo imagem, IArmazenamentoArquivoGateway armazenamentoArquivoGateway);
    Task RemoverImagemAsync(Guid produtoId, IArmazenamentoArquivoGateway armazenamentoArquivoGateway);
}
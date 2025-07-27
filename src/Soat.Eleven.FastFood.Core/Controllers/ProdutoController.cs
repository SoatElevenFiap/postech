using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.DTOs.Produtos;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class ProdutoController
{
    private readonly IProdutoGateway _produtoGateway;

    public ProdutoController(IProdutoGateway produtoGateway)
    {
        _produtoGateway = produtoGateway;
    }

    public async Task<IEnumerable<ResumoProdutoDto>> ListarProdutos(Guid? categoriaId, bool incluirInativos, ICategoriaProdutoDataSource categoriaGateway)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        IEnumerable<Produto> result = await useCase.ListarProdutos(categoriaGateway, incluirInativos, categoriaId);

        return result.Select(ProdutoPresenter.Output);
    }

    public async Task<ResumoProdutoDto> GetProduto(Guid id)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        var result = await useCase.ObterProdutoPorId(id);

        return ProdutoPresenter.Output(result);
    }

    public async Task<ResumoProdutoDto> CriarProduto(CriarProdutoDto criarProduto, ICategoriaProdutoDataSource categoriaGateway)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        var entity = ProdutoPresenter.Input(criarProduto);
        var result = await useCase.CriarProduto(entity, categoriaGateway);

        return ProdutoPresenter.Output(result!);
    }

    public async Task<ResumoProdutoDto> AtualizarProduto(AtualizarProdutoDto atualizarProduto)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        var entity = ProdutoPresenter.Input(atualizarProduto);
        var result = await useCase.AtualizarProduto(entity);

        return ProdutoPresenter.Output(result);
    }

    public async Task DesativarProduto(Guid id)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        await useCase.DesativarProduto(id);
    }

    public async Task ReativarProduto(Guid id)
    {
        var useCase = new ProdutoUseCase(_produtoGateway);
        await useCase.ReativarProduto(id);
    }

    //FALTA UPLOAD DE IMAGEM
}

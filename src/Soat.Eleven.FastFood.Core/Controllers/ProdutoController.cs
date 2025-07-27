using Soat.Eleven.FastFood.Common.DTOs.Produtos;
using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class ProdutoController
{
    private readonly IProdutoDataSource _produtoDataSource;
    private ICategoriaProdutoDataSource _categoriaDataSource;

    public ProdutoController(IProdutoDataSource produtoGateway, ICategoriaProdutoDataSource categoriaProdutoDataSource)
    {
        _produtoDataSource = produtoGateway;
        _categoriaDataSource = categoriaProdutoDataSource;
    }

    private ProdutoUseCase FabricarUseCase()
    {
        var produtoGateway = new ProdutoGateway(_produtoDataSource);
        var categoriaGateway = new CategoriaProdutoGateway(_categoriaDataSource);
        return ProdutoUseCase.Create(produtoGateway, categoriaGateway);
    }

    public async Task<IEnumerable<ProdutoDto>> ListarProdutos(Guid? categoriaId, bool incluirInativos)
    {
        var useCase = FabricarUseCase();

        IEnumerable<Produto> result = await useCase.ListarProdutos(incluirInativos, categoriaId);

        return result.Select(ProdutoPresenter.Output);
    }

    public async Task<ProdutoDto?> GetProduto(Guid id)
    {
        var useCase = FabricarUseCase();
        var result = await useCase.ObterProdutoPorId(id);

        if (result == null)
            return null;

        return ProdutoPresenter.Output(result);
    }

    public async Task<ProdutoDto> CriarProduto(CriarProdutoDto criarProduto)
    {
        var useCase = FabricarUseCase();

        var entity = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = criarProduto.Nome,
            SKU = criarProduto.SKU,
            Descricao = criarProduto.Descricao,
            Preco = criarProduto.Preco,
            CategoriaId = criarProduto.CategoriaId,
            Ativo = true,
            CriadoEm = DateTime.UtcNow,
            Imagem = criarProduto.Imagem
        };

        var result = await useCase.CriarProduto(entity);

        return ProdutoPresenter.Output(result!);
    }

    public async Task<ProdutoDto> AtualizarProduto(AtualizarProdutoDto atualizarProduto)
    {
        var useCase = FabricarUseCase();

        var entity = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = atualizarProduto.Nome,
            SKU = atualizarProduto.SKU,
            Descricao = atualizarProduto.Descricao,
            Preco = atualizarProduto.Preco,
            CategoriaId = atualizarProduto.CategoriaId,
            Ativo = true,
            CriadoEm = DateTime.UtcNow,
            Imagem = atualizarProduto.Imagem
        };


        var result = await useCase.AtualizarProduto(entity);

        return ProdutoPresenter.Output(result);
    }

    public async Task DesativarProduto(Guid id)
    {
        var useCase = FabricarUseCase();
        await useCase.DesativarProduto(id);
    }

    public async Task ReativarProduto(Guid id)
    {
        var useCase = FabricarUseCase();
        await useCase.ReativarProduto(id);
    }

    //FALTA UPLOAD DE IMAGEM
}

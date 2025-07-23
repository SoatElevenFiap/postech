using Soat.Eleven.FastFood.Core.DTOs.Categorias;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class CategoriaController
{
    private readonly ICategoriaGateway _categoriaGateway;

    public CategoriaController(ICategoriaGateway categoriaGateway)
    {
        _categoriaGateway = categoriaGateway;
    }

    public async Task<IEnumerable<ResumoCategoriaDto>> ListarCategorias(bool incluirInativos)
    {
        var useCase = new CategoriaUseCase(_categoriaGateway);
        var result = await useCase.ListarCategorias(incluirInativos);
        return result.Select(CategoriaPresenter.Output);
    }

    public async Task<ResumoCategoriaDto> GetCategoriaPorId(Guid id)
    {
        var useCase = new CategoriaUseCase(_categoriaGateway);
        var result = await useCase.ObterCategoriaPorId(id);

        return CategoriaPresenter.Output(result);
    }

    public async Task<ResumoCategoriaDto> CriarCategoria(CriarCategoriaDto criarCategoria)
    {
        var entity = CategoriaPresenter.Input(criarCategoria);
        var useCase = new CategoriaUseCase(_categoriaGateway);
        var result = await useCase.CriarCategoria(entity);

        return CategoriaPresenter.Output(result);
    }

    public async Task<ResumoCategoriaDto> AtualizarCategoria(AtualizarCategoriaDto atualizarCategoria)
    {
        var entity = CategoriaPresenter.Input(atualizarCategoria);
        var useCase = new CategoriaUseCase(_categoriaGateway);
        var result = await useCase.AtualizarCategoria(entity);

        return CategoriaPresenter.Output(result);
    }

    public async Task DesativarCategoria(Guid id)
    {
        var useCase = new CategoriaUseCase(_categoriaGateway);
        await useCase.DesativarCategoria(id);
    }

    public async Task ReativarCategoria(Guid id)
    {
        var useCase = new CategoriaUseCase(_categoriaGateway);
        await useCase.ReativarCategoria(id);
    }
}

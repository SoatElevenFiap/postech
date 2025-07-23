using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class CategoriaUseCase : ICategoriaUseCase
{
    private readonly ICategoriaGateway _categoriaGateway;

    public CategoriaUseCase(ICategoriaGateway categoriaGateway)
    {
        _categoriaGateway = categoriaGateway;
    }

    public async Task<IEnumerable<CategoriaProduto>> ListarCategorias(bool? incluirInativos = false)
    {
        var categorias = incluirInativos == true ?
            await _categoriaGateway.GetAllAsync() :
            await _categoriaGateway.FindAsync(c => c.Ativo);

        return categorias;
    }

    public async Task<CategoriaProduto?> ObterCategoriaPorId(Guid id)
    {
        var categoria = await _categoriaGateway.GetByIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException();

        return categoria;
    }

    public async Task<CategoriaProduto> CriarCategoria(CategoriaProduto categoria)
    {
        var existeCategoria = await _categoriaGateway.FindAsync(c => c.Nome == categoria.Nome);
        if (existeCategoria.Any())
            throw new ArgumentException("Categoria de mesmo nome já existe");

        var novaCategoria = new CategoriaProduto
        {
            Id = Guid.NewGuid(),
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = true
        };

        var categoriaCriada = await _categoriaGateway.AddAsync(novaCategoria);

        return categoriaCriada;
    }

    public async Task<CategoriaProduto> AtualizarCategoria(CategoriaProduto categoria)
    {
        var categoriaExistente = await _categoriaGateway.GetByIdAsync(categoria.Id);
        if (categoriaExistente == null)
            throw new KeyNotFoundException("Categoria não encontrada");

        categoriaExistente.Nome = categoria.Nome;
        categoriaExistente.Descricao = categoria.Descricao;

        await _categoriaGateway.UpdateAsync(categoriaExistente);

        return categoriaExistente;
    }

    public async Task DesativarCategoria(Guid id)
    {
        var categoria = await _categoriaGateway.GetByIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoria não encontrada");

        categoria.Ativo = false;
        await _categoriaGateway.UpdateAsync(categoria);
    }

    public async Task ReativarCategoria(Guid id)
    {
        var categoria = await _categoriaGateway.GetByIdAsync(id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoria não encontrada");

        categoria.Ativo = true;
        await _categoriaGateway.UpdateAsync(categoria);
    }
}

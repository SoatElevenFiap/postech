using Soat.Eleven.FastFood.Core.DTOs.Produtos;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public class ProdutoPresenter
{
    public static Produto Input(CriarProdutoDto? input)
    {
        try
        {
            return (Produto)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Produto Input(AtualizarProdutoDto? input)
    {
        try
        {
            return (Produto)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static ResumoProdutoDto Output(Produto? output)
    {
        try
        {
            return (ResumoProdutoDto)output!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

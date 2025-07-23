using Soat.Eleven.FastFood.Core.DTOs.Categorias;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Presenters;

public class CategoriaPresenter
{
    public static CategoriaProduto Input(object? input)
    {
        try
        {
            return (CategoriaProduto)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static ResumoCategoriaDto Output(CategoriaProduto? output)
    {
        try
        {
            return (ResumoCategoriaDto)output!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

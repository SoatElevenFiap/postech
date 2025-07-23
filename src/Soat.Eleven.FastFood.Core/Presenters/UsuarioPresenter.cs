namespace Soat.Eleven.FastFood.Core.Presenters;

public static class UsuarioPresenter
{
    public static T Input<T>(object? input) where T : class
    {
        try
        {
            return (T)input!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static T Output<T>(object? output) where T : class
    {
        try
        {
            return (T)output!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

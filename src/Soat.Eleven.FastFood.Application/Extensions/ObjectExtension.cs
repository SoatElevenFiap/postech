namespace Soat.Eleven.FastFood.Application.Extensions;

public static class ObjectExtension
{
    public static T TranslateTo<T>(this object obj) where T : class
    {
        return (T)obj;
    }
}

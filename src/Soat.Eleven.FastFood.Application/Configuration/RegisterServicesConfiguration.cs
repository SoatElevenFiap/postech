using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
        serviceCollection.AddScoped<ICategoriaService, CategoriaService>();
        serviceCollection.AddScoped<IProdutoService, ProdutoService>();
    }
}

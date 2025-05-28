using FluentValidation;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Application.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
        serviceCollection.AddScoped<ICategoriaService, CategoriaService>();
        serviceCollection.AddScoped<IPedidoService, PedidoService>();
        serviceCollection.AddScoped<IProdutoService, ProdutoService>();
        serviceCollection.AddScoped<ITokenAtendimentoService, TokenAtendimentoService>();
        serviceCollection.AddScoped<IPagamentoService, PagamentoService>();
    }

    public static void RegisterValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<Usuario>, UsuarioValidation>();
    }
}

using FluentValidation;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
    }

    public static void RegisterValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<Usuario>, UsuarioValidation>();
    }
}

using FluentValidation;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Application.Services.Interfaces;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Core.Application.Ports.Inputs;

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
        serviceCollection.AddScoped<IImagemService, ImagemService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
    }

    public static void RegisterValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<Usuario>, UsuarioValidation>();
    }
}

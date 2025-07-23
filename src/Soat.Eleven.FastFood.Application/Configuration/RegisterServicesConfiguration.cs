using FluentValidation;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Gateways;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Infrastructure.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {        
        // Gateways
        serviceCollection.AddScoped<IUsuarioGateway, UsuarioGateway>();
        serviceCollection.AddScoped<IClienteGateway, ClienteGateway>();
        serviceCollection.AddScoped<ICategoriaGateway, CategoriaGateway>();
        serviceCollection.AddScoped<IPedidoGateway, PedidoGateway>();
        serviceCollection.AddScoped<IProdutoGateway, ProdutoGateway>();
        serviceCollection.AddScoped<ITokenAtendimentoGateway, TokenAtendimentoGateway>();
        serviceCollection.AddScoped<IPagamentoGateway, PagamentoGateway>();
        //serviceCollection.AddScoped<IArmazenamentoArquivoGateway, ArmazenamentoArquivoGateway>();
        
        // Services
        serviceCollection.AddScoped<IImagemService, ImagemService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
    }

    public static void RegisterValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<Usuario>, UsuarioValidation>();
    }
}

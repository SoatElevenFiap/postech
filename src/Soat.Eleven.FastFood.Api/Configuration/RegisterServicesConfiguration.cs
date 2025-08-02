using Soat.Eleven.FastFood.Adapter.Infra.DataSources;
using Soat.Eleven.FastFood.Adapter.Infra.Gateways;
using Soat.Eleven.FastFood.Adapter.Infra.Services;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Infra.Gateways;
using Soat.Eleven.FastFood.Infrastructure.Gateways;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        #region Data Sources
        serviceCollection.AddScoped<ICategoriaProdutoDataSource, CategoriaProdutoDataSource>();
        serviceCollection.AddScoped<IProdutoDataSource, ProdutoDataSource>();
        serviceCollection.AddScoped<IPedidoDataSource, PedidoDataSource>();
        //serviceCollection.AddScoped<IClienteDataSource, ClienteDataSource>(); está dando erro erro no momento de injetar o serviço
        #endregion

        // Gateways
        /*serviceCollection.AddScoped<IUsuarioGateway, UsuarioGateway>();
        serviceCollection.AddScoped<IAdministradorGateway, AdministradorGateway>();        
        serviceCollection.AddScoped<IPedidoDataSource, PedidoDataSource>();
        
        serviceCollection.AddScoped<ITokenAtendimentoGateway, TokenAtendimentoGateway>();
        serviceCollection.AddScoped<IPagamentoGateway, PagamentoGateway>();
        //serviceCollection.AddScoped<IArmazenamentoArquivoGateway, ArmazenamentoArquivoGateway>();*/

        // Services
        //serviceCollection.AddScoped<IImagemService, ImagemService>();
        serviceCollection.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}

using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class TokenAtendimentoController
{
    private readonly ITokenAtendimentoDataSource _tokenAtendimentoDataSource;

    public TokenAtendimentoController(ITokenAtendimentoDataSource tokenAtendimentoDataSource)
    {
        _tokenAtendimentoDataSource = tokenAtendimentoDataSource;
    }

    public TokenAtendimentoUseCase FabricarUseCase()
    {
        var tokenAtendimentoGateway = new TokenAtendimentoGateway(_tokenAtendimentoDataSource);
        return TokenAtendimentoUseCase.Create(tokenAtendimentoGateway);
    }

    public async Task<string> GerarTokenPorCpf(string cpf, IJwtTokenService jwtTokenService, IUsuarioDataSource usuarioGateway)
    {
        var useCase = FabricarUseCase();
        var token = await useCase.GetTokenPorCPF(cpf,usuarioGateway);
        return jwtTokenService.GenerateToken(token);
    }

    public async Task<string> GerarTokenAnonimo(IJwtTokenService jwtTokenService)
    {
        var useCase = FabricarUseCase();
        var token = await useCase.GetTokenAnonimo();
        return jwtTokenService.GenerateToken(token);
    }
}

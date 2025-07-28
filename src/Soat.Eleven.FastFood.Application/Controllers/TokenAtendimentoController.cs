using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class TokenAtendimentoController
{
    private readonly ITokenAtendimentoGateway _tokenAtendimentoGateway;

    public TokenAtendimentoController(ITokenAtendimentoGateway tokenAtendimentoGateway)
    {
        _tokenAtendimentoGateway = tokenAtendimentoGateway;
    }

    public async Task<string> GerarTokenPorCpf(string cpf, IJwtTokenService jwtTokenService, IUsuarioGateway usuarioGateway)
    {
        var useCase = new TokenAtendimentoUseCase(_tokenAtendimentoGateway);
        return await useCase.GetTokenPorCPF(cpf, jwtTokenService, usuarioGateway);
    }

    public async Task<string> GerarTokenAnonimo(IJwtTokenService jwtTokenService)
    {
        var useCase = new TokenAtendimentoUseCase(_tokenAtendimentoGateway);
        return await useCase.GetTokenAnonimo(jwtTokenService);
    }
}

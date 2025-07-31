using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
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
        var token = await useCase.GetTokenPorCPF(cpf,usuarioGateway);
        return jwtTokenService.GenerateToken(token);
    }

    public async Task<string> GerarTokenAnonimo(IJwtTokenService jwtTokenService)
    {
        var useCase = new TokenAtendimentoUseCase(_tokenAtendimentoGateway);
        var token = await useCase.GetTokenAnonimo();
        return jwtTokenService.GenerateToken(token);
    }
}

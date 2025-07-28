using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class AuthController
{
    private readonly IUsuarioGateway _usuarioGateway;

    public AuthController(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<string> Login(AuthUsuarioRequestDto requestDto, IJwtTokenService jwtTokenService, IPasswordService passwordService)
    {
        var useCase = new AuthUseCase(_usuarioGateway);
        return await useCase.Login(requestDto, jwtTokenService, passwordService);
    }
}

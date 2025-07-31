using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class AuthController
{
    private readonly IUsuarioGateway _usuarioGateway;

    public AuthController(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<string> Login(AuthUsuarioRequestDto requestDto, IJwtTokenService jwtTokenService)
    {
        var useCase = new AuthUseCase(_usuarioGateway);
        var usuario = await useCase.Login(requestDto, jwtTokenService.GetIdUsuario());
        return jwtTokenService.GenerateToken(new Dtos.UsuarioDto(usuario.Id,usuario.Nome,usuario.Email,usuario.Perfil));

    }
}

using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class AuthController
{
    private readonly IUsuarioDataSource _usuarioDataSource;

    public AuthController(IUsuarioDataSource usuarioGateway)
    {
        _usuarioDataSource = usuarioGateway;
    }

    private UsuarioUseCase FabricarUseCase()
    {
        var usuarioGateway = new UsuarioGateway(_usuarioDataSource);
        return UsuarioUseCase.Create(usuarioGateway);
    }

    public async Task<string> Login(AuthUsuarioRequestDto requestDto, IJwtTokenService jwtTokenService)
    {
        var useCase = FabricarUseCase();
        var usuario = await useCase.Login(requestDto.Email, requestDto.Senha);

        return jwtTokenService.GenerateToken(new Dtos.UsuarioDto(usuario.Id,usuario.Nome,usuario.Email,usuario.Perfil));
    }
}

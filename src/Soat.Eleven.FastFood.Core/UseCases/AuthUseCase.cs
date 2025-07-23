using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUsuarioGateway _usuarioGateway;

    public AuthUseCase(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<string> Login(AuthUsuarioRequestDto authUsuarioRequestDto, IJwtTokenService jwtTokenService, IPasswordService passwordService)
    {
        var usuario = await _usuarioGateway.GetByEmailAsync(authUsuarioRequestDto.Email);

        if (usuario is null)
            throw new ArgumentException("E-mail e/ou Senha estão incorretos");

        if (!passwordService.Equal(authUsuarioRequestDto.Senha, usuario.Senha))
            throw new ArgumentException("E-mail e/ou Senha estão incorretos");

        var token = jwtTokenService.GenerateToken(usuario);

        return token;
    }
}

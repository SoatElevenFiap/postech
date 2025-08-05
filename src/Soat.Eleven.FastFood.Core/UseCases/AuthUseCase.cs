using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Gateways;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class AuthUseCase
{
    private readonly UsuarioGateway _usuarioGateway;

    public AuthUseCase(UsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<Usuario> Login(AuthUsuarioRequestDto authUsuarioRequestDto, Guid usuarioId)
    {
        var usuario = await _usuarioGateway.ObterUsuarioPodId(usuarioId) ?? throw new ArgumentException("E-mail e/ou Senha estão incorretos");

        if (!Usuario.ItIsMyPassword(authUsuarioRequestDto.Senha, usuario.Senha))
            throw new ArgumentException("E-mail e/ou Senha estão incorretos");

        return usuario;
    }
}

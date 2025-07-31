using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Entities;
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

    public async Task<Usuario> Login(AuthUsuarioRequestDto authUsuarioRequestDto, Guid usuarioId)
    {
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        if (usuario is null)
            throw new ArgumentException("E-mail e/ou Senha estão incorretos");

        if (!usuario.ItIsMyPassword(authUsuarioRequestDto.Senha, usuario.Senha))
            throw new ArgumentException("E-mail e/ou Senha estão incorretos");


        return usuario;
    }
}

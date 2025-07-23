using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;
// usecase retorna entidade ou lista
// na controller , converte para DTO (ou responseReturn) e retorna
public class UsuarioUseCase : IUsuarioUseCase
{
    private readonly IUsuarioGateway _usuarioGateway;

    public UsuarioUseCase(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<Usuario> AlterarSenha(string newPassword, string currentPassword, IJwtTokenService jwtTokenService, IPasswordService passwordService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        if (usuario is null)
            throw new Exception("Usuário não encontrado");

        if (passwordService.Generate(currentPassword) != usuario.Senha)
            throw new Exception("Senha atual está incorreta");

        usuario.Senha = passwordService.Generate(newPassword);

        await _usuarioGateway.AddAsync(usuario);

        return usuario;
    }
}

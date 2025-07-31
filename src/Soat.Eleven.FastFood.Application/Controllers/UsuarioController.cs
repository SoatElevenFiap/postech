using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class UsuarioController
{
    private readonly IUsuarioGateway _usuarioGateway;

    public UsuarioController(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<bool> AtualizarSenha(AtualizarSenhaRequestDto dto,Guid usuarioId)
    {
        var useCase = new UsuarioUseCase(_usuarioGateway);
        await useCase.AlterarSenha(dto.NewPassword, dto.CurrentPassword, usuarioId);

        // Aceito sugestões
        return true;
    }
}

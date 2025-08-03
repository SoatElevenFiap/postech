using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class UsuarioController
{
    private readonly IUsuarioDataSource _usuarioDataSource;
    private readonly IJwtTokenService _jwtTokenGateway;

    public UsuarioController(IUsuarioDataSource usuarioDataSource, IJwtTokenService jwtTokenGateway)
    {
        _usuarioDataSource = usuarioDataSource;
        _jwtTokenGateway = jwtTokenGateway;
    }

    private UsuarioUseCase FabricarUseCase()
    {
        var usuarioGateway = new UsuarioGateway(_usuarioDataSource);
        return UsuarioUseCase.Create(usuarioGateway);
    }

    public async Task<UsuarioAdmResponseDto> InserirAdministradorAsync(CriarAdmRequestDto administrador)
    {
        var entity = UsuarioPresenter.Input(administrador);
        var useCase = FabricarUseCase();
        var result = await useCase.InserirAdministrador(entity);

        return UsuarioPresenter.Output(result);
    }

    public async Task<bool> AtualizarSenha(AtualizarSenhaRequestDto dto,Guid usuarioId)
    {
        var useCase = FabricarUseCase();
        await useCase.AlterarSenha(dto.NewPassword, dto.CurrentPassword, usuarioId);

        return true;
    }

    public async Task<object?> AtualizarAdministradorAsync(AtualizarAdmRequestDto request, IJwtTokenService jwtTokenGateway)
    {
        var useCase = FabricarUseCase();

        //var result = await useCase.InserirAdministrador(request);

        var result = request; 

        return result;
    }

    public async Task<object?> GetAdministradorAsync(IJwtTokenService jwtTokenGateway)
    {
        var userId = jwtTokenGateway.GetIdUsuario();
        var useCase = FabricarUseCase();        
        var result = await useCase.ObterUsuarioPodId(userId);

        return result;
    }
}

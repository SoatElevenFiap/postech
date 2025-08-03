using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class AdministradorController
{
    public readonly IAdministradorDataSource _administradorGateway;

    public AdministradorController(IAdministradorDataSource administradorGateway)
    {
        _administradorGateway = administradorGateway;
    }

    public async Task<UsuarioAdmResponseDto> InserirAdministradorAsync(CriarAdmRequestDto administrador)
    {
        var entity = UsuarioPresenter.Input(administrador);
        var useCase = new AdministradorUseCase(_administradorGateway);
        var result = await useCase.InserirAdministrador(entity);

        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioAdmResponseDto> AtualizarAdministradorAsync(AtualizarAdmRequestDto dto, IJwtTokenService jwtTokenService)
    {
        var entity = UsuarioPresenter.Input(dto);
        var useCase = new AdministradorUseCase(_administradorGateway);
        var result = await useCase.AtualizarAdministrador(entity, jwtTokenService.GetIdUsuario());

        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioAdmResponseDto> GetAdministradorAsync(IJwtTokenService jwtTokenService)
    {
        var useCase = new AdministradorUseCase(_administradorGateway);
        var result = await useCase.GetAdministrador(jwtTokenService.GetIdUsuario());
        return UsuarioPresenter.Output(result);
    }
}

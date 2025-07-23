using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class AdministradorController
{
    public readonly IAdministradorGateway _administradorGateway;

    public AdministradorController(IAdministradorGateway administradorGateway)
    {
        _administradorGateway = administradorGateway;
    }

    public async Task<Administrador> InserirAdministradorAsync(CriarAdmRequestDto administrador)
    {
        var entity = UsuarioPresenter.Input<Administrador>(administrador);
        var useCase = new AdministradorUseCase(_administradorGateway);
        return await useCase.InserirAdministrador(entity);
    }

    public async Task<UsuarioAdmResponseDto> AtualizarAdministradorAsync(AtualizarAdmRequestDto dto, IJwtTokenService jwtTokenService)
    {
        var entity = UsuarioPresenter.Input<Administrador>(dto);
        var useCase = new AdministradorUseCase(_administradorGateway);
        var result = await useCase.AtualizarAdministrador(entity, jwtTokenService);

        return UsuarioPresenter.Output<UsuarioAdmResponseDto>(result);
    }

    public async Task<UsuarioAdmResponseDto> GetAdministradorAsync(IJwtTokenService jwtTokenService)
    {
        var useCase = new AdministradorUseCase(_administradorGateway);
        var result = await useCase.GetAdministrador(jwtTokenService);
        return UsuarioPresenter.Output<UsuarioAdmResponseDto>(result);
    }
}

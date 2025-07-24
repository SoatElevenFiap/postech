using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Core.Controllers;

public class ClienteController
{
    private readonly IClienteGateway _clienteGateway;

    public ClienteController(IClienteGateway clienteGateway)
    {
        _clienteGateway = clienteGateway;
    }

    public async Task<string> InserirClienteAsync(CriarClienteRequestDto dto, IJwtTokenService jwtTokenService)
    {
        var entity = UsuarioPresenter.Input(dto);
        var useCase = new ClienteUseCase(_clienteGateway);

        return await useCase.InserirCliente(entity, jwtTokenService);
    }

    public async Task<UsuarioClienteResponseDto> AtualizarClienteAsync(AtualizarClienteRequestDto dto, IJwtTokenService jwtTokenService)
    {
        var entity = UsuarioPresenter.Input(dto);
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.AtualizarCliente(entity, jwtTokenService);

        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetClienteAsync(IJwtTokenService jwtTokenService)
    {
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.GetCliente(jwtTokenService);
        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetByCPF(string cpf)
    {
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.GetClienteByCPF(cpf);
        return UsuarioPresenter.Output(result);
    }
}

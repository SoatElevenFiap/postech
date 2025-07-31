using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class ClienteController
{
    private readonly IClienteGateway _clienteGateway;
    private readonly IJwtTokenService _jwtTokenService;

    public ClienteController(IClienteGateway clienteGateway, IJwtTokenService jwtTokenService)
    {
        _clienteGateway = clienteGateway;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> InserirClienteAsync(CriarClienteRequestDto dto)
    {
        var entity = UsuarioPresenter.Input(dto);
        var useCase = new ClienteUseCase(_clienteGateway);
        var cliente = await useCase.InserirCliente(entity);
        var jwtToken = _jwtTokenService.GenerateToken(new Dtos.UsuarioDto(cliente.Id,cliente.Nome,cliente.Email,cliente.Perfil), string.Empty);
        return jwtToken;
    }

    public async Task<UsuarioClienteResponseDto> AtualizarClienteAsync(AtualizarClienteRequestDto dto)
    {
        var entity = UsuarioPresenter.Input(dto);
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.AtualizarCliente(entity, _jwtTokenService.GetIdUsuario());

        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetClienteAsync()
    {
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.GetCliente(_jwtTokenService.GetIdUsuario());
        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetByCPF(string cpf)
    {
        var useCase = new ClienteUseCase(_clienteGateway);
        var result = await useCase.GetClienteByCPF(cpf);
        return UsuarioPresenter.Output(result);
    }
}

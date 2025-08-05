using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Presenters;
using Soat.Eleven.FastFood.Core.UseCases;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class ClienteController
{
    private readonly IClienteDataSource _clienteDataSource;
    private readonly IJwtTokenService _jwtTokenService;

    public ClienteController(IClienteDataSource clienteGateway, IJwtTokenService jwtTokenService)
    {
        _clienteDataSource = clienteGateway;
        _jwtTokenService = jwtTokenService;
    }

    private ClienteUseCase FabricarUseCase()
    {
        var clienteGateway = new ClienteGateway(_clienteDataSource);
        return ClienteUseCase.Create(clienteGateway);
    }

    public async Task<string> InserirClienteAsync(CriarClienteRequestDto dto)
    {
        var entity = UsuarioPresenter.Input(dto);

        var useCase = FabricarUseCase();
        var cliente = await useCase.InserirCliente(entity);
        var jwtToken = _jwtTokenService.GenerateToken(new Dtos.UsuarioDto(cliente.Id,cliente.Nome,cliente.Email,cliente.Perfil), string.Empty);
        return jwtToken;
    }

    public async Task<UsuarioClienteResponseDto> AtualizarClienteAsync(AtualizarClienteRequestDto dto)
    {
        var entity = UsuarioPresenter.Input(dto);

        var useCase = FabricarUseCase();
        var result = await useCase.AtualizarCliente(entity, _jwtTokenService.GetIdUsuario());

        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetClienteAsync()
    {
        var useCase = FabricarUseCase();
        var result = await useCase.GetCliente(_jwtTokenService.GetIdUsuario());
        return UsuarioPresenter.Output(result);
    }

    public async Task<UsuarioClienteResponseDto> GetByCPF(string cpf)
    {
        var useCase = FabricarUseCase();
        var result = await useCase.GetClienteByCPF(cpf);
        return UsuarioPresenter.Output(result);
    }
}

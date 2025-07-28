using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Adapter.WebApi.Controllers;

[ApiController]
[Route("api/Cliente")]
public class ClienteRestEndpoints : ControllerBase
{
    private readonly IClienteGateway _clienteGateway;
    private readonly IJwtTokenService _jwtTokenService;

    public ClienteRestEndpoints(IClienteGateway clienteGateway, IJwtTokenService jwtTokenService)
    {
        _clienteGateway = clienteGateway;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> InserirCliente([FromBody] CriarClienteRequestDto request)
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.InserirClienteAsync(request, _jwtTokenService));
    }

    [HttpPut("{id}")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, [FromBody] AtualizarClienteRequestDto request)
    {
        request.Id = id;
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.AtualizarClienteAsync(request, _jwtTokenService));
    }

    [HttpGet]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> GetUsuario()
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.GetClienteAsync(_jwtTokenService));
    }

    [HttpGet("PorCpf/{cpf}")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> GetUsuario([FromRoute] string cpf)
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.GetByCPF(cpf));
    }
}

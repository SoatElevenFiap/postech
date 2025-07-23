using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Core.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Adapter.WebApi.Controllers;

[ApiController]
[Route("api")]
public class ClienteRestController : ControllerBase
{
    private readonly IClienteGateway _clienteGateway;
    private readonly IJwtTokenService _jwtTokenService;

    public ClienteRestController(IClienteGateway clienteGateway, IJwtTokenService jwtTokenService)
    {
        _clienteGateway = clienteGateway;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("cliente")]
    public async Task<IActionResult> InserirCliente([FromBody] CriarClienteRequestDto request)
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.InserirClienteAsync(request, _jwtTokenService));
    }

    [HttpPut("cliente")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> AtualizarCliente([FromBody] AtualizarClienteRequestDto request)
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.AtualizarClienteAsync(request, _jwtTokenService));
    }

    [HttpGet("cliente")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> GetUsuario()
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.GetClienteAsync(_jwtTokenService));
    }

    [HttpGet("cliente/PorCpf/{cpf}")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> GetUsuario([FromRoute] string cpf)
    {
        var controller = new ClienteController(_clienteGateway);
        return Ok(await controller.GetByCPF(cpf));
    }
}

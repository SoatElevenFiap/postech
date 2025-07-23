using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Core.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Adapter.WebApi.Controllers;

[Route("api")]
public class AdministradorRestController : BaseController
{
    private readonly IAdministradorGateway _administradorGateway;
    private readonly IJwtTokenService _jwtTokenGateway;

    public AdministradorRestController(IAdministradorGateway administradorGateway, IJwtTokenService jwtTokenGateway)
    {
        _administradorGateway = administradorGateway;
        _jwtTokenGateway = jwtTokenGateway;
    }

    [HttpPost("Administrador")]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> InserirAdministrador([FromBody] CriarAdmRequestDto request)
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.InserirAdministradorAsync(request));
    }

    [HttpPut("Administrador")]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> AtualizarAdministrador([FromBody] AtualizarAdmRequestDto request)
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.AtualizarAdministradorAsync(request, _jwtTokenGateway));
    }

    [HttpGet("Administrador")]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> GetAdministrador()
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.GetAdministradorAsync(_jwtTokenGateway));
    }
}

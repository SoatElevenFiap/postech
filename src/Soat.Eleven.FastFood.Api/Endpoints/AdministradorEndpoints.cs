using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Adapter.WebApi.Controllers;

[ApiController]
[Route("api/Administrador")]
public class AdministradorEndpoints : ControllerBase
{
    private readonly IAdministradorGateway _administradorGateway;
    private readonly IJwtTokenService _jwtTokenGateway;

    public AdministradorEndpoints(IAdministradorGateway administradorGateway, IJwtTokenService jwtTokenGateway)
    {
        _administradorGateway = administradorGateway;
        _jwtTokenGateway = jwtTokenGateway;
    }

    [HttpPost]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> InserirAdministrador([FromBody] CriarAdmRequestDto request)
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.InserirAdministradorAsync(request));
    }

    [HttpPut]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> AtualizarAdministrador([FromBody] AtualizarAdmRequestDto request)
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.AtualizarAdministradorAsync(request, _jwtTokenGateway));
    }

    [HttpGet]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> GetAdministrador()
    {
        var controller = new AdministradorController(_administradorGateway);
        return Ok(await controller.GetAdministradorAsync(_jwtTokenGateway));
    }
}

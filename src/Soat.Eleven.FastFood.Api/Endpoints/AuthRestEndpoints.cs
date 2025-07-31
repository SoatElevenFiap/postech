using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthRestEndpoints : ControllerBase
{
    private readonly IUsuarioGateway _usuarioGateway;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthRestEndpoints(IUsuarioGateway usuarioGateway,
                           IJwtTokenService jwtTokenService)
    {
        _usuarioGateway = usuarioGateway;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUsuario([FromBody] AuthUsuarioRequestDto request)
    {
        var controller = new AuthController(_usuarioGateway);
        var result = await controller.Login(request, _jwtTokenService);

        return Ok(result);
    }
}

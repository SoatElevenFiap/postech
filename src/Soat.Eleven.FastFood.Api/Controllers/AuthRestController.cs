using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Core.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthRestController : ControllerBase
{
    private readonly IUsuarioGateway _usuarioGateway;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordService _passwordService;

    public AuthRestController(IUsuarioGateway usuarioGateway,
                          IPasswordService passwordService,
                          IJwtTokenService jwtTokenService)
    {
        _usuarioGateway = usuarioGateway;
        _passwordService = passwordService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUsuario([FromBody] AuthUsuarioRequestDto request)
    {
        var controller = new AuthController(_usuarioGateway);
        var result = await controller.Login(request, _jwtTokenService, _passwordService);

        return Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;

namespace Soat.Eleven.FastFood.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthRestEndpoints : ControllerBase
{
    private readonly IUsuarioDataSource _usuarioGateway;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthRestEndpoints(IUsuarioDataSource usuarioGateway,
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

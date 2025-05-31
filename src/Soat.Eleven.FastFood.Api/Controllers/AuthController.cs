using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUsuario([FromBody] AuthUsuarioRequestDto request)
    {
        return SendReponse(await _authService.LoginUsuario(request));
    }
}

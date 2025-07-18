using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.Ports.Inputs;

namespace Soat.Eleven.FastFood.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthUseCase _authUseCase;

    public AuthController(IAuthUseCase authUseCase)
    {
        _authUseCase = authUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUsuario([FromBody] AuthUsuarioRequestDto request)
    {
        return SendReponse(await _authUseCase.LoginUsuario(request));
    }
}

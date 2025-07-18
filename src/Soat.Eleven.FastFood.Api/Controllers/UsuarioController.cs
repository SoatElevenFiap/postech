using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers;

[Route("api/[controller]")]
public class UsuarioController : BaseController
{
    private readonly IUsuarioUseCase _usuarioUseCase;

    public UsuarioController(IUsuarioUseCase usuarioUseCase)
    {
        _usuarioUseCase = usuarioUseCase;
    }

    [HttpPost("Cliente")]
    public async Task<IActionResult> InserirCliente([FromBody] CriarClienteRequestDto request)
    {
        return SendReponse(await _usuarioUseCase.InserirCliente(request));
    }

    [HttpPut("Cliente")]
    [Authorize(PolicyRole.Cliente)]
    public async Task<IActionResult> AtualizarCliente([FromBody] AtualizarClienteRequestDto request)
    {
        return SendReponse(await _usuarioUseCase.AtualizarCliente(request));
    }

    [HttpPost("Administrador")]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> InserirAdministrador([FromBody] CriarAdmRequestDto request)
    {
        return SendReponse(await _usuarioUseCase.InserirAdministrador(request));
    }

    [HttpPut("Administrador")]
    [Authorize(PolicyRole.Administrador)]
    public async Task<IActionResult> AtualizarAdministrador([FromBody] AtualizarAdmRequestDto request)
    {
        return SendReponse(await _usuarioUseCase.AtualizarAdministrador(request));
    }

    [HttpGet]
    [Authorize(PolicyRole.Commom)]
    public async Task<IActionResult> GetUsuario()
    {
        return SendGetResponse(await _usuarioUseCase.GetUsuario());
    }
      

    [HttpPut("Password")]
    public async Task<IActionResult> AtualizarSenha([FromBody] AtualizarSenhaRequestDto request)
    {
        return SendReponse(await _usuarioUseCase.AlterarSenha(request));
    }

    [HttpGet("Cliente/PorCpf/{cpf}")]
    public async Task<IActionResult> GetClientePorCpf([FromRoute] string cpf)
    {
        return SendReponse(await _usuarioUseCase.GetClientePorCpf(cpf));
    }
}

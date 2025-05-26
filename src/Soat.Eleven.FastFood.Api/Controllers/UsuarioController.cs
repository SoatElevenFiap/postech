using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Cliente")]
        public async Task<IActionResult> InserirCliente([FromBody] CriarClienteRequestDto request)
        {
            return SendReponse(await _usuarioService.InserirCliente(request));
        }

        [HttpPut("Cliente/{id}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, [FromBody] AtualizarClienteRequestDto request)
        {
            return SendReponse(await _usuarioService.AtualizarCliente(id, request));
        }

        [HttpPost("Administrador")]
        public async Task<IActionResult> InserirAdministrador([FromBody] CriarAdmRequestDto request)
        {
            return SendReponse(await _usuarioService.InserirAdministrador(request));
        }

        [HttpPut("Administrador/{id}")]
        public async Task<IActionResult> AtualizarAdministrador([FromRoute] Guid id, [FromBody] AtualizarAdmRequestDto request)
        {
            return SendReponse(await _usuarioService.AtualizarAdministrador(id, request));
        }

        [HttpGet("Cliente/{id}")]
        public async Task<IActionResult> GetCliente([FromRoute] Guid id)
        {
            return SendReponse(await _usuarioService.GetUsuario(id));
        }

        [HttpPut("Password/{id}")]
        public async Task<IActionResult> AtualizarSenha([FromRoute] Guid id, [FromBody] AtualizarSenhaRequestDto request)
        {
            return SendReponse(await _usuarioService.AlterarSenha(id, request));
        }
    }
}

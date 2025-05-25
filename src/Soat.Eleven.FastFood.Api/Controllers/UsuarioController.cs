using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> InserirCliente([FromBody] CriarClienteRequestDto request)
        {
            var usuario = await _usuarioService.InserirCliente(request);

            return SendReponse(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, [FromBody] AtualizarClienteDto request)
        {
            return SendReponse(await _usuarioService.AtualizarCliente(id, request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario([FromRoute] Guid id)
        {
            return SendReponse(await _usuarioService.GetCliente(id));
        }
    }
}

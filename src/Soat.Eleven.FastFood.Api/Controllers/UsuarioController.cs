using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs.Usuario.Request;
using Soat.Eleven.FastFood.Application.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> InserirCliente([FromBody] CriarClienteDto request)
        {
            var usuario = await _usuarioService.InserirCliente(request);

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] Guid id, [FromBody] AtualizarClienteDto request)
        {
            //return Ok(await _usuarioService.AtualizarCliente(id, request));
            return Ok("Atualizado");
        }
    }
}

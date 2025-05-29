using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Services.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtentidmentoController : ControllerBase
    {
        private readonly ITokenAtendimentoService _tokenService;
        private readonly ILogger<AtentidmentoController> _logger;
        private readonly IUsuarioService _usuarioService;

        public AtentidmentoController(ITokenAtendimentoService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [HttpGet("token/porCpf/{cpf}")]
        public async Task<IActionResult> GerarTokenPrCpf([FromRoute] string cpf)
        {
            var tokenAtendimentoDTO = await _tokenService.GerarToken(null, cpf);

            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/porClientId/{ClientId}")]
        public async Task<IActionResult> GerarTokePorClienteId([FromRoute] Guid ClientId)
        {
            var user = await _usuarioService.GetUsuario(ClientId);
            
            if (user == null)
                return NotFound("Usuario não encontrado por ClientId");

            var tokenAtendimentoDTO = await _tokenService.GerarToken(ClientId);

            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/{cpf}/{clienteId}")]
        public async Task<IActionResult> GerarToken([FromRoute] string cpf, [FromRoute] Guid clienteId)
        {
            var tokenAtendimentoDTO = await _tokenService.GerarToken(clienteId, cpf);

            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);

        }

        [HttpGet("token/anonimo")]
        public async Task<IActionResult> GerarTokenAnonimo()
        {
            var token = await _tokenService.GerarToken();
            return Ok(new { Token = token });
        }
    }
}
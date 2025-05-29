using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtentidmentoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenAtendimentoService _tokenService;


        public AtentidmentoController(IUsuarioService usuarioService,
                                      ITokenAtendimentoService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpGet("token/porCpf/cpf")]
        public async Task<IActionResult> GerarTokenPrCpf([FromRoute] string cpf)
        {
            var tokenAtendimentoDTO = await _tokenService.GerarToken(null, cpf);
           
            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/{cpf}/{clienteId}")]
        public async Task<IActionResult> GerarToken([FromRoute]  string cpf, [FromRoute] Guid clienteId)
        {
            var tokenAtendimentoDTO = await _tokenService.GerarToken(clienteId, cpf);
          
            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);

        }

        [HttpPost("token/anonimo")]
        public IActionResult GerarTokenAnonimo()
        {
            var token = _tokenService.GerarToken();
            return Ok(new { Token = token });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services.Interfaces;

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

        /// <summary>
        /// Não é necessario usuario Cadastrado para gerar o token de atendimento.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("token/porCpf/{cpf}")]
        public async Task<IActionResult> GerarTokenPorCpf([FromRoute] string cpf)
        {
            var tokenAtendimentoDTO = await _tokenService.GerarToken(null, cpf);

            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);
        }

        /// <summary>
        /// Gera o token de atendimento por ClientId, então, nesse caso o ClientId deve existir.
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        [HttpGet("token/porClientId/{UsuarioId}")]
        public async Task<IActionResult> GerarTokePorClienteId([FromRoute] Guid UsuarioId)
        {
            var user = await _usuarioService.GetUsuario(UsuarioId);
            
            if (user == null)
                return NotFound("Usuario não encontrado");

            var ClientId = (user.Data as UsuarioClienteResponseDto)?.ClientId;
            var tokenAtendimentoDTO = await _tokenService.GerarToken(ClientId);

            if (tokenAtendimentoDTO != null)
                return Ok(tokenAtendimentoDTO);

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/{cpf}/{usuarioId}")]
        public async Task<IActionResult> GerarToken([FromRoute] string Cpf, [FromRoute] Guid usuarioId)
        {
            var user = await _usuarioService.GetUsuario(usuarioId);
            if (!user.Success)
                return NotFound("Usuario não encontrado");

            var ClientId = (user.Data as UsuarioClienteResponseDto)?.ClientId;
            var tokenAtendimentoDTO = await _tokenService.GerarToken(ClientId, Cpf);

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
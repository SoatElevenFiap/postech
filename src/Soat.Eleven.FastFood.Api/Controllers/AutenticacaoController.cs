using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Services.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    /// <summary>
    /// Controller provisorio, deve ser alterado para o controller de autenticação.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly ITokenAtendimentoService _tokenAtendimentoService;

        public AutenticacaoController(ILogger<AutenticacaoController> logger, ITokenAtendimentoService tokenAtendimentoService)
        {
            _logger = logger;
            _tokenAtendimentoService = tokenAtendimentoService;
        }

        [HttpPost("Logar")]
        public async Task<IActionResult> CriarAutenticacao()
        {
            
            return Ok("Autenticacao criada com sucesso.");
        }

        [HttpPost("RecuperarToken")]
        public async Task<IActionResult> RecuperarToken()
        {
            return Ok("Autenticacao criada com sucesso.");
        }

        [HttpPost("ValidarToken")]
        public async Task<IActionResult> ValidarToken()
        {
            return Ok("Autenticacao criada com sucesso.");
        }
    }
}

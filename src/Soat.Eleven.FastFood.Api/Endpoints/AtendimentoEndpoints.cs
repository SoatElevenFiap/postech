using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class AtendimentoEndpoints : ControllerBase
    {
        private readonly ILogger<AtendimentoEndpoints> _logger;
        private readonly ITokenAtendimentoGateway _tokenAtendimentoGateway;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsuarioGateway _usuarioGateway;

        public AtendimentoEndpoints(ILogger<AtendimentoEndpoints> logger,
                                     ITokenAtendimentoGateway tokenAtendimentoGateway,
                                     IJwtTokenService jwtTokenService,
                                     IUsuarioGateway usuarioGateway)
        {
            _logger = logger;
            _tokenAtendimentoGateway = tokenAtendimentoGateway;
            _jwtTokenService = jwtTokenService;
            _usuarioGateway = usuarioGateway;
        }

        /// <summary>
        /// N�o � necessario usuario Cadastrado para gerar o token de atendimento.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("porCpf/{cpf}")]
        public async Task<IActionResult> GerarTokenPorCpf([FromRoute] string cpf)
        {
            var controller = new TokenAtendimentoController(_tokenAtendimentoGateway);
            return Ok(await controller.GerarTokenPorCpf(cpf, _jwtTokenService, _usuarioGateway));
        }

        [HttpGet("anonimo")]
        public async Task<IActionResult> GerarTokenAnonimo()
        {
            var controller = new TokenAtendimentoController(_tokenAtendimentoGateway);
            return Ok(await controller.GerarTokenAnonimo(_jwtTokenService));
        }
    }
}

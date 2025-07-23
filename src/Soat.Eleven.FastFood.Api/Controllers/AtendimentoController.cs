using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Core.Controllers;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [Route("api")]
    public class AtendimentoController : BaseController
    {
        private readonly ILogger<AtendimentoController> _logger;
        private readonly ITokenAtendimentoGateway _tokenAtendimentoGateway;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsuarioGateway _usuarioGateway;

        public AtendimentoController(ILogger<AtendimentoController> logger,
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
        [HttpGet("token/porCpf/{cpf}")]
        public async Task<IActionResult> GerarTokenPorCpf([FromRoute] string cpf)
        {
            var controller = new TokenAtendimentoController(_tokenAtendimentoGateway);
            return Ok(await controller.GerarTokenPorCpf(cpf, _jwtTokenService, _usuarioGateway));
        }

        [HttpGet("token/anonimo")]
        public async Task<IActionResult> GerarTokenAnonimo()
        {
            var controller = new TokenAtendimentoController(_tokenAtendimentoGateway);
            return Ok(await controller.GerarTokenAnonimo(_jwtTokenService));
        }
    }
}

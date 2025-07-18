using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Gateways;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [Route("api/[controller]")]
    public class AtendimentoController : BaseController
    {
        private readonly ITokenAtendimentoUseCase _tokenUseCase;
        private readonly ILogger<AtendimentoController> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsuarioGateway _usuarioGateway;

        public AtendimentoController(ITokenAtendimentoUseCase tokenUseCase,
                                     IJwtTokenService jwtTokenService,
                                     IUsuarioGateway usuarioGateway)
        {
            _tokenUseCase = tokenUseCase;
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
            var tokenAtendimentoDTO = await _tokenUseCase.GerarToken(null, cpf);

            if (tokenAtendimentoDTO != null)
            {
                string jwtToken;

                if (tokenAtendimentoDTO.ClienteId is null)
                {
                    jwtToken = _jwtTokenService.GenerateToken(tokenAtendimentoDTO.TokenId.ToString());
                    return SendReponse(ResultResponse.SendSuccess(jwtToken));
                }

                var usuario = await _usuarioGateway.GetByIdAsync(tokenAtendimentoDTO.ClienteId.Value);
                jwtToken = _jwtTokenService.GenerateToken(usuario!, tokenAtendimentoDTO.TokenId.ToString());
                return SendReponse(ResultResponse.SendSuccess(jwtToken));
            }

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/anonimo")]
        public async Task<IActionResult> GerarTokenAnonimo()
        {
            var token = await _tokenUseCase.GerarToken();

            var jwtToken = _jwtTokenService.GenerateToken(token.TokenId.ToString());
            return SendReponse(ResultResponse.SendSuccess(jwtToken));
        }
    }
}

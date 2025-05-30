using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Api.Configuration;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Services.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [Route("api/[controller]")]
    public class AtendimentoController : BaseController
    {
        private readonly ITokenAtendimentoService _tokenService;
        private readonly ILogger<AtendimentoController> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRepository<Usuario> _usuarioRepository;

        public AtendimentoController(ITokenAtendimentoService tokenService,
                                     IJwtTokenService jwtTokenService,
                                     IRepository<Usuario> usuarioRepository)
        {
            _tokenService = tokenService;
            _jwtTokenService = jwtTokenService;
            _usuarioRepository = usuarioRepository;
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
            {
                string jwtToken;

                if (tokenAtendimentoDTO.ClienteId is null)
                {
                    jwtToken = _jwtTokenService.GenerateToken(tokenAtendimentoDTO.TokenId.ToString());
                    return SendReponse(ResultResponse.SendSuccess(jwtToken));
                }

                var usuario = (await _usuarioRepository.FindAsync(x => x.Cliente.Id == tokenAtendimentoDTO.ClienteId, u => u.Include(c => c.Cliente))).FirstOrDefault();
                jwtToken = _jwtTokenService.GenerateToken(usuario!, tokenAtendimentoDTO.TokenId.ToString());
                return SendReponse(ResultResponse.SendSuccess(jwtToken));
            }

            return BadRequest(tokenAtendimentoDTO);
        }

        [HttpGet("token/anonimo")]
        public async Task<IActionResult> GerarTokenAnonimo()
        {
            var token = await _tokenService.GerarToken();

            var jwtToken = _jwtTokenService.GenerateToken(token.TokenId.ToString());
            return SendReponse(ResultResponse.SendSuccess(jwtToken));
        }
    }
}
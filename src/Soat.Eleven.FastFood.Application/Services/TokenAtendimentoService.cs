using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento.Mappers;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public class TokenAtendimentoService : ITokenAtendimentoService
    {
        private readonly IRepository<TokenAtendimento> _tokenRepository;
        private readonly ILogger<TokenAtendimentoService> _logger;

        public TokenAtendimentoService(
            IRepository<TokenAtendimento> tokenRepository,
            ILogger<TokenAtendimentoService> logger)
        {
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gera o Token de atendimento para o cliente.
        /// com ou sem identificação do cliente.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public async Task<TokenAtendimentoDTO> GerarToken(Guid? clienteId, string? cpf)
        {
            try
            {
                var token = new TokenAtendimento
                {
                    TokenId = Guid.NewGuid(),
                    ClienteId = clienteId,
                    Cpf = cpf,
                    CriadoEm = DateTime.UtcNow
                };
                await _tokenRepository.AddAsync(token);

                return TokenAtendimentoMapper.MapToDto(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token de atendimento.");
                throw;
            }
        }


        public TokenAtendimentoDTO RecuperarTokenAtendimento(Guid tokenId)
        {
            try
            {
                var token = _tokenRepository.GetByIdAsync(tokenId).Result;
                if (token == null)
                {
                    _logger.LogWarning("Token não encontrado para o id: {TokenId}", tokenId);
                    throw new Exception("Token não encontrado.");
                }

                return TokenAtendimentoMapper.MapToDto(token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar token de atendimento.");
                throw;
            }
        }

        public async Task<TokenAtendimentoDTO> PersistirTokenAtendimento(TokenAtendimentoDTO token)
        {
            try
            {
                await _tokenRepository.AddAsync(TokenAtendimentoMapper.MapToEntity(token));
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao persistir token de atendimento.");
                throw;
            }
        }
        public async Task<TokenAtendimentoDTO?> RecuperarTokenMaisNovoPorCpfAsync(string cpf)
        {
            try
            {
                var tokens = await _tokenRepository.FindAsync(
                    t => t.Cpf == cpf
                );

                var tokenMaisNovo = tokens
                    .OrderByDescending(t => t.CriadoEm)
                    .FirstOrDefault();

                if (tokenMaisNovo == null)
                {
                    _logger.LogWarning("Nenhum token encontrado para o CPF: {Cpf}", cpf);
                    return null;
                }


                return TokenAtendimentoMapper.MapToDto(tokenMaisNovo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar o token mais novo por CPF.");
                throw;
            }
        }
    }
}

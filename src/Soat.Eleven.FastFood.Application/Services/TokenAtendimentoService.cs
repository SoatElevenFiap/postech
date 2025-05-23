using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.DTOs;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public class TokenAtendimentoService : ITokenAtendimentoService
    {
        private readonly IRepository<TokenAtendimento> _pedidoRepository;
        private readonly ILogger<TokenAtendimentoService> _logger;

        public TokenAtendimentoService(
            IRepository<TokenAtendimento> pedidoRepository,
            ILogger<TokenAtendimentoService> logger)
        {
            _pedidoRepository = pedidoRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gera o Token de atendimento para o cliente.
        /// com ou sem identificação do cliente.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public TokenAtendimentoDTO GerarToken(Guid? clienteId, string? cpf)
        {
            try
            {
                return new TokenAtendimentoDTO
                {
                    TokenId = Guid.NewGuid(),
                    ClienteId = clienteId,
                    Cpf = cpf,
                    CriadoEm = DateTime.UtcNow
                };
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
                var token = _pedidoRepository.GetByIdAsync(tokenId).Result;
                if (token == null)
                {
                    _logger.LogWarning("Token não encontrado para o id: {TokenId}", tokenId);
                    throw new Exception("Token não encontrado.");
                }
                return new TokenAtendimentoDTO()
                {
                    TokenId = token.TokenId,
                    ClienteId = token.ClienteId,
                    Cpf = token.Cpf,
                    CriadoEm = token.CriadoEm
                }
                ;
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
                await _pedidoRepository.AddAsync(
                                               new TokenAtendimento()
                                               {
                                                   TokenId = token.TokenId,
                                                   ClienteId = token.ClienteId,
                                                   Cpf = token.Cpf,
                                                   CriadoEm = token.CriadoEm
                                               });

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
                var tokens = await _pedidoRepository.FindAsync(
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

                return new TokenAtendimentoDTO
                {
                    TokenId = tokenMaisNovo.TokenId,
                    ClienteId = tokenMaisNovo.ClienteId,
                    Cpf = tokenMaisNovo.Cpf,
                    CriadoEm = tokenMaisNovo.CriadoEm
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar o token mais novo por CPF.");
                throw;
            }
        }
    }
}

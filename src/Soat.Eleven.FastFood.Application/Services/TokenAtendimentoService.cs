using Microsoft.Extensions.Logging;
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
        public TokenAtendimento GerarToken(Guid? clienteId, string? cpf)
        {
            try
            {
                return new TokenAtendimento
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


        public TokenAtendimento RecuperarTokenAtendimento(Guid tokenId)
        {
            try
            {
                var token = _pedidoRepository.GetByIdAsync(tokenId).Result;
                if (token == null)
                {
                    _logger.LogWarning("Token não encontrado para o id: {TokenId}", tokenId);
                    throw new Exception("Token não encontrado.");
                }
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar token de atendimento.");
                throw;
            }
        }

        public async Task<TokenAtendimento> PersistirTokenAtendimento(TokenAtendimento token)
        {
            try
            {
                return await _pedidoRepository.AddAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao persistir token de atendimento.");
                throw;
            }
        }
    }
}

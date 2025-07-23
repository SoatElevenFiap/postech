using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Core.Application.Mappers;
using Soat.Eleven.FastFood.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.UseCases
{
    public class TokenAtendimentoUseCase : ITokenAtendimentoUseCase
    {
        private readonly ITokenAtendimentoGateway _tokenGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly ILogger<TokenAtendimentoUseCase> _logger;

        public TokenAtendimentoUseCase(
            ITokenAtendimentoGateway tokenGateway,
            ILogger<TokenAtendimentoUseCase> logger,
            IUsuarioGateway usuarioGateway)
        {
            _tokenGateway = tokenGateway;
            _logger = logger;
            _usuarioGateway = usuarioGateway;
        }

        /// <summary>
        /// Gera o Token de atendimento para o cliente.
        /// com ou sem identificação do cliente.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private async Task<TokenAtendimentoDTO> TokenGen(Guid? clienteId = default, string? cpf = default)
        {
            try
            {
                var token = new TokenAtendimento
                {
                    TokenId = Guid.NewGuid(),
                    ClienteId = clienteId,
                    CpfCliente = cpf,
                    CriadoEm = DateTime.UtcNow,
                };

                await _tokenGateway.AddAsync(token);

                //return TokenAtendimentoMapper.MapToDto(token);
                return new TokenAtendimentoDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token de atendimento");
                throw;
            }
        }

        public async Task<TokenAtendimentoDTO> GerarToken(Guid? clienteId = default, string? cpf = default)
        {
            return await TokenGen(clienteId, cpf);
        }

        public TokenAtendimentoDTO RecuperarTokenAtendimento(Guid tokenId)
        {
            try
            {
                var token = _tokenGateway.GetByIdAsync(tokenId).Result;

                //return token != null ? TokenAtendimentoMapper.MapToDto(token) : throw new Exception("Token não encontrado");
                return new TokenAtendimentoDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar token de atendimento");
                throw;
            }
        }

        public async Task<TokenAtendimentoDTO?> RecuperarTokenMaisNovoPorCpfAsync(string cpf)
        {
            try
            {
                var token = await _tokenGateway.GetMaisRecentePorCpfAsync(cpf);

                //return token != null ? TokenAtendimentoMapper.MapToDto(token) : null;
                return new TokenAtendimentoDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar token mais novo por CPF");
                throw;
            }
        }

        public async Task<TokenAtendimentoDTO> GerarToken(Cliente cliente)
        {
            try
            {
                return await GerarToken(cliente.Id, cliente.Cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token para cliente");
                throw;
            }
        }
    }
}

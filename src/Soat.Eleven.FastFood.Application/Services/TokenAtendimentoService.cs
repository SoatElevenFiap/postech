using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Core.Application.Mappers;
using Soat.Eleven.FastFood.Core.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public class TokenAtendimentoService : ITokenAtendimentoService
    {
        private readonly IRepository<TokenAtendimento> _tokenRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger<TokenAtendimentoService> _logger;

        public TokenAtendimentoService(
            IRepository<TokenAtendimento> tokenRepository,
            ILogger<TokenAtendimentoService> logger,
            IRepository<Usuario> usuarioRepository)
        {
            _tokenRepository = tokenRepository;
            _logger = logger;
            _usuarioRepository = usuarioRepository;
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
                    Cpf = cpf,
                    CriadoEm = DateTime.UtcNow,
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

        public async Task<TokenAtendimentoDTO> GerarToken(Guid? clienteId, string? cpf)
        {
            try
            {
                if (clienteId != null & cpf != null)
                    return await TokenGen(clienteId, cpf);

                if (clienteId == null && !string.IsNullOrEmpty(cpf))
                {
                    var response = (await _usuarioRepository.FindAsync(x => x.Cliente.Cpf == cpf, u => u.Include(c => c.Cliente))).FirstOrDefault();
                    if (response != null)
                    {
                        var user = (UsuarioClienteResponseDto)response;
                        clienteId = user.ClientId;

                        return await GerarToken(response.Cliente);
                    }
                }

                if (clienteId != null && string.IsNullOrEmpty(cpf))
                {
                    var response = await _usuarioRepository.GetByIdAsync(clienteId.Value, u => u.Cliente);
                    if (response is not null)
                    {
                        var user = (UsuarioClienteResponseDto)response;
                        cpf = user.Cpf;
                    }

                }

                return await TokenGen(clienteId, cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar gerar novo de atendimento token");
                throw;

            }

        }

        public async Task<TokenAtendimentoDTO> GerarToken(Cliente cliente)
        {
            try
            {
                return await TokenGen(cliente.Id, cliente.Cpf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar gerar novo de atendimento token");
                throw;
            }
        }
    }
}

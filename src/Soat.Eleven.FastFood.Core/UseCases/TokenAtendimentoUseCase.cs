using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases
{
    public class TokenAtendimentoUseCase : ITokenAtendimentoUseCase
    {
        private readonly ITokenAtendimentoGateway _tokenGateway;

        public TokenAtendimentoUseCase(
            ITokenAtendimentoGateway tokenGateway)
        {
            _tokenGateway = tokenGateway;
        }

        public async Task<string> GetTokenPorCPF(string cpf, IJwtTokenService jwtTokenService, IUsuarioGateway usuarioGateway)
        {
            var tokenAtendimentoDTO = await GerarToken(null, cpf);

            if (tokenAtendimentoDTO != null)
            {
                string jwtToken;

                if (tokenAtendimentoDTO.ClienteId is null)
                {
                    jwtToken = jwtTokenService.GenerateToken(tokenAtendimentoDTO.TokenId.ToString());
                    return jwtToken;
                }

                var usuario = await usuarioGateway.GetByIdAsync(tokenAtendimentoDTO.ClienteId.Value);
                jwtToken = jwtTokenService.GenerateToken(usuario!, tokenAtendimentoDTO.TokenId.ToString());
                return jwtToken;
            }

            throw new Exception("Token não gerado");
        }

        public async Task<string> GetTokenAnonimo(IJwtTokenService jwtTokenService)
        {
            var token = await GerarToken();

            var jwtToken = jwtTokenService.GenerateToken(token.TokenId.ToString());
            return jwtToken;
        }

        /// <summary>
        /// Gera o Token de atendimento para o cliente.
        /// com ou sem identificação do cliente.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private async Task<TokenAtendimento> TokenGen(Guid? clienteId = default, string? cpf = default)
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

                return token;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao gerar token de atendimento");
                throw;
            }
        }

        public async Task<TokenAtendimento> GerarToken(Guid? clienteId = default, string? cpf = default)
        {
            return await TokenGen(clienteId, cpf);
        }

        public TokenAtendimento RecuperarTokenAtendimento(Guid tokenId)
        {
            try
            {
                var token = _tokenGateway.GetByIdAsync(tokenId).Result;

                return token ?? throw new Exception("Token não encontrado");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao recuperar token de atendimento");
                throw;
            }
        }

        public async Task<TokenAtendimento?> RecuperarTokenMaisNovoPorCpfAsync(string cpf)
        {
            try
            {
                var token = await _tokenGateway.GetMostRecentTokenByCpfAsync(cpf);

                return token;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao recuperar token mais novo por CPF");
                throw;
            }
        }

        public async Task<TokenAtendimento> GerarToken(Cliente cliente)
        {
            try
            {
                return await GerarToken(cliente.Id, cliente.Cpf);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao gerar token para cliente");
                throw;
            }
        }
    }
}

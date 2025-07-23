using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;
public interface ITokenAtendimentoUseCase
{
    Task<string> GetTokenPorCPF(string cpf, IJwtTokenService jwtTokenService, IUsuarioGateway usuarioGateway);
    Task<string> GetTokenAnonimo(IJwtTokenService jwtTokenService);
    Task<TokenAtendimento> GerarToken(Guid? clienteId = default, string? cpf = default);
    TokenAtendimento RecuperarTokenAtendimento(Guid tokenId);
    Task<TokenAtendimento?> RecuperarTokenMaisNovoPorCpfAsync(string cpf);
    Task<TokenAtendimento> GerarToken(Cliente cliente);
}

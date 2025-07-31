using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;
public interface ITokenAtendimentoUseCase
{
    Task<string> GetTokenPorCPF(string cpf, IUsuarioGateway usuarioGateway);
    Task<string> GetTokenAnonimo();
    Task<TokenAtendimento> GerarToken(Guid? clienteId = default, string? cpf = default);
    TokenAtendimento RecuperarTokenAtendimento(Guid tokenId);
    Task<TokenAtendimento?> RecuperarTokenMaisNovoPorCpfAsync(string cpf);
    Task<TokenAtendimento> GerarToken(Cliente cliente);
}

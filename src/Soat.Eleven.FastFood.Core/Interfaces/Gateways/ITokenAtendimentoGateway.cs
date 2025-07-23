using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface ITokenAtendimentoGateway
{
    Task AddAsync(TokenAtendimento tokenAtendimento);
    Task<TokenAtendimento?> GetByIdAsync(Guid tokenId);
    Task<TokenAtendimento> GetMostRecentTokenByCpfAsync(string cpf);
}

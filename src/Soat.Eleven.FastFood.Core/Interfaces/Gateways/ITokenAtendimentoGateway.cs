using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface ITokenAtendimentoGateway
{
    Task Save(TokenAtendimento tokenAtendimento);
    Task<TokenAtendimento> GetById(Guid tokenId);
    Task<TokenAtendimento> GetMostRecentTokenByCpfAsync(string cpf);
}

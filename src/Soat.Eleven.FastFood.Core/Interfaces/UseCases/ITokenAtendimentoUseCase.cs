using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;
public interface ITokenAtendimentoUseCase
{
    Task<TokenAtendimento> GerarToken(Guid? clienteId = default, string? cpf = default);
    TokenAtendimento RecuperarTokenAtendimento(Guid tokenId);
    Task<TokenAtendimento?> RecuperarTokenMaisNovoPorCpfAsync(string cpf);
    Task<TokenAtendimento> GerarToken(Cliente cliente);
}

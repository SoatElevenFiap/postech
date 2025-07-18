using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface ITokenAtendimentoGateway
    {
        Task<TokenAtendimento> AddAsync(TokenAtendimento entity);
        Task<TokenAtendimento?> GetByIdAsync(Guid id);
        Task<TokenAtendimento?> GetByTokenAsync(string token);
        Task<TokenAtendimento?> GetMaisRecentePorCpfAsync(string cpf);
        Task<IEnumerable<TokenAtendimento>> GetAllAsync();
        Task UpdateAsync(TokenAtendimento entity);
        Task DeleteAsync(TokenAtendimento entity);
    }
}

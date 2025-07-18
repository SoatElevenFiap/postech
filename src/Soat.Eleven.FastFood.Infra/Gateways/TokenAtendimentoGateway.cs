using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;

namespace Soat.Eleven.FastFood.Infrastructure.Gateways
{
    public class TokenAtendimentoGateway : ITokenAtendimentoGateway
    {
        private readonly List<TokenAtendimento> _tokens = new();

        public Task<TokenAtendimento> AddAsync(TokenAtendimento entity)
        {
            _tokens.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<TokenAtendimento?> GetByIdAsync(Guid id)
        {
            var token = _tokens.FirstOrDefault(t => t.TokenId == id);
            return Task.FromResult(token);
        }

        public Task<TokenAtendimento?> GetByTokenAsync(string token)
        {
            var result = _tokens.FirstOrDefault(t => t.TokenId.ToString() == token);
            return Task.FromResult(result);
        }

        public Task<TokenAtendimento?> GetMaisRecentePorCpfAsync(string cpf)
        {
            var result = _tokens
                .Where(t => t.Cpf == cpf || t.CpfCliente == cpf)
                .OrderByDescending(t => t.CriadoEm)
                .FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<TokenAtendimento>> GetAllAsync()
        {
            return Task.FromResult(_tokens.AsEnumerable());
        }

        public Task UpdateAsync(TokenAtendimento entity)
        {
            var index = _tokens.FindIndex(t => t.TokenId == entity.TokenId);
            if (index >= 0)
            {
                _tokens[index] = entity;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TokenAtendimento entity)
        {
            _tokens.RemoveAll(t => t.TokenId == entity.TokenId);
            return Task.CompletedTask;
        }
    }
}

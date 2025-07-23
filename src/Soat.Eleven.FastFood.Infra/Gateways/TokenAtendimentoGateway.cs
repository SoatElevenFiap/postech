using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Adapter.Infra.Gateways;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infrastructure.Gateways
{
    public class TokenAtendimentoGateway : GatewayBase<TokenAtendimentoModel>, ITokenAtendimentoGateway
    {
        public TokenAtendimentoGateway(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TokenAtendimento entity)
        {
            var model = Parse(entity);

            await AddModelAsync(model);
        }

        public async Task<TokenAtendimento?> GetByIdAsync(Guid id)
        {
            var exist = await FindModelAsync(
                x => x.TokenId == id);

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task<TokenAtendimento> GetMostRecentTokenByCpfAsync(string cpf)
        {
            var result = await FindModelAsync(t => t.Cpf == cpf || t.CpfCliente == cpf);
            return Parse(result.OrderByDescending(t => t.CriadoEm)
                    .FirstOrDefault());
        }

        private static TokenAtendimentoModel Parse(TokenAtendimento entity)
        {
            var model = new TokenAtendimentoModel()
            {
                TokenId = entity.TokenId,
                ClienteId = entity.ClienteId,
                Cpf = entity.Cpf
            };
            return model;
        }

        private static TokenAtendimento Parse(TokenAtendimentoModel model)
        {
            return new TokenAtendimento()
            {
                TokenId = model.TokenId,
                ClienteId = model.ClienteId,
                Cpf = model.Cpf,
            };
        }
    }
}

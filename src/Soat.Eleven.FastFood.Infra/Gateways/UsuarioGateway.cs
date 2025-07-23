using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Adapter.Infra.Gateways;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class UsuarioGateway : GatewayBase<UsuarioModel>, IUsuarioGateway
    {
        public UsuarioGateway(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario> AddAsync(Usuario cliente)
        {
            var model = Parse(cliente);

            await AddModelAsync(model);

            return Parse(model);
        }

        public Task DeleteAsync(Usuario entity)
        {
            var model = Parse(entity);
            return DeleteModelAsync(model);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var result = await GetAllModelAsync();
            return result.Select(Parse);
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            var exist = await FindModelAsync(
                x => x.Id == id);

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task UpdateAsync(Usuario entity)
        {
            var model = Parse(entity);

            await UpdateModelAsync(model);
        }

        private static UsuarioModel Parse(Usuario entity)
        {
            var model = new UsuarioModel(
                entity.Nome,
                entity.Email,
                entity.Senha,
                entity.Telefone,
                entity.Perfil);
            return model;
        }

        private static Usuario Parse(UsuarioModel model)
        {
            return new Usuario(model.Nome,
                               model.Email,
                               model.Senha,
                               model.Telefone,
                               model.Perfil,
                               model.Status);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Adapter.Infra.Gateways;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class ClienteGateway : GatewayBase<UsuarioModel>, IClienteGateway
    {
        public ClienteGateway(AppDbContext context) : base(context)
        {
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            var model = Parse(cliente);

            await AddModelAsync(model);

            return Parse(model);
        }

        public async Task DeleteAsync(Cliente entity)
        {
            var model = Parse(entity);
            await DeleteModelAsync(model);
        }

        public async Task<bool> ExistCpf(string cpf)
        {
            var exist = await FindModelAsync(
                x => x.Cliente.Cpf == cpf,
                query => query.Include(x => x.Cliente));

            return exist.Any();
        }

        public async Task<bool> ExistEmail(string email)
        {
            var exist = await FindModelAsync(
                x => x.Email == email);

            return exist.Any();
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var result = await GetAllModelAsync();
            return result.Select(Parse);
        }

        public async Task<Cliente?> GetByCPF(string cpf)
        {
            var exist = await FindModelAsync(
                x => x.Cliente.Cpf == cpf,
                query => query.Include(x => x.Cliente));

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            var exist = await FindModelAsync(
                x => x.Cliente.Id == id,
                query => query.Include(x => x.Cliente));

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task<Cliente?> GetByUsuarioId(Guid usuarioId)
        {
            var exist = await FindModelAsync(
                x => x.Id == usuarioId,
                query => query.Include(x => x.Cliente));

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task UpdateAsync(Cliente entity)
        {
            var model = Parse(entity);

            await UpdateModelAsync(model);
        }

        private static UsuarioModel Parse(Cliente cliente)
        {
            var usuario = new UsuarioModel(
                cliente.Nome,
                cliente.Email,
                cliente.Senha,
                cliente.Telefone,
                cliente.Perfil);
            usuario.CriarCliente(cliente.Cpf, cliente.DataDeNascimento);
            return usuario;
        }

        private static Cliente Parse(UsuarioModel model)
        {
            return new Cliente(model.Id,model.Nome,
                               model.Email,
                               model.Senha,
                               model.Telefone,
                               model.Perfil,
                               model.Status,
                               model.Cliente.Cpf,
                               model.Cliente.DataDeNascimento);
        }
    }
}

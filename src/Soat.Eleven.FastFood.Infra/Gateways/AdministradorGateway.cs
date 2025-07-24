using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Adapter.Infra.Gateways;

public class AdministradorGateway : GatewayBase<UsuarioModel>, IAdministradorGateway
{
    public AdministradorGateway(AppDbContext context) : base(context)
    {
    }

    public async Task<Administrador> AddAsync(Administrador entity)
    {
        var model = Parse(entity);

        await AddModelAsync(model);

        return Parse(model);
    }

    public async Task DeleteAsync(Administrador entity)
    {
        var model = Parse(entity);
        await DeleteModelAsync(model);
    }

    public async Task<bool> ExistEmail(string email)
    {
        var exist = await FindModelAsync(
                x => x.Email == email);

        return exist.Any();
    }

    public async Task<IEnumerable<Administrador>> GetAllAsync()
    {
        var result = await GetAllModelAsync();
        return result.Select(Parse);
    }

    public async Task<Administrador?> GetByIdAsync(Guid id)
    {
        var exist = await FindModelAsync(
                x => x.Id == id);

        return exist.Any() ? Parse(exist.First()) : null;
    }

    public Task UpdateAsync(Administrador pedido)
    {
        throw new NotImplementedException();
    }

    private static UsuarioModel Parse(Administrador entity)
    {
        var usuario = new UsuarioModel(
            entity.Nome,
            entity.Email,
            entity.Senha,
            entity.Telefone,
            entity.Perfil);
        return usuario;
    }

    private static Administrador Parse(UsuarioModel model)
    {
        return new Administrador(model.Id, model.Nome,
                           model.Email,
                           model.Senha,
                           model.Telefone,
                           model.Perfil,
                           model.Status);
    }
}

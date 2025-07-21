using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IUsuarioGateway
{
    Task<bool> ExistEmail(string email);
    Task<bool> ExistCpf(string cpf);
    Task<Cliente> SaveCliente(Cliente cliente);
    Task<Cliente> GetClienteByUsuarioId(Guid usuarioId);
    Task<Cliente> GetClienteByCPF(string cpf);
    Task<Administrador> SaveAdministrador(Administrador usuario);
    Task<Administrador> GetAdminstradorByUsuarioId(Guid usuarioId);
    Task<Usuario> GetUsuarioById(Guid usuarioId);
    Task<Usuario> Save(Usuario usuario);
}

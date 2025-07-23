using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IClienteGateway : IGateway<Cliente>
{
    Task<bool> ExistEmail(string email);
    Task<bool> ExistCpf(string cpf);
    Task<Cliente?> GetByUsuarioId(Guid usuarioId);
    Task<Cliente?> GetByCPF(string cpf);
}

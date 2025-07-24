using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.Gateways;

public interface IAdministradorGateway : IGateway<Administrador>
{
    Task<bool> ExistEmail(string email);
}

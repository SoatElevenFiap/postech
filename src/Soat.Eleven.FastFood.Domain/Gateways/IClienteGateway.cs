using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface IClienteGateway
    {
        Task<Cliente> AddAsync(Cliente entity);
        Task<Cliente?> GetByIdAsync(Guid id);
        Task<Cliente?> GetByCpfAsync(string cpf);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task UpdateAsync(Cliente entity);
        Task DeleteAsync(Cliente entity);
    }
}

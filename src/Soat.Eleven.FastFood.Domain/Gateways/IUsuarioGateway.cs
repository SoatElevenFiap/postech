using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface IUsuarioGateway
    {
        Task<Usuario> AddAsync(Usuario entity);
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task UpdateAsync(Usuario entity);
        Task DeleteAsync(Usuario entity);
    }
}

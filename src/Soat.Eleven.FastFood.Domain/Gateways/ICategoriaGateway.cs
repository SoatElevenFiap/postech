using Soat.Eleven.FastFood.Domain.Entidades;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface ICategoriaGateway
    {
        Task<CategoriaProduto> AddAsync(CategoriaProduto entity);
        Task<CategoriaProduto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CategoriaProduto>> GetAllAsync();
        Task<IEnumerable<CategoriaProduto>> FindAsync(Expression<Func<CategoriaProduto, bool>> predicate);
        Task UpdateAsync(CategoriaProduto entity);
        Task DeleteAsync(CategoriaProduto entity);
    }
}

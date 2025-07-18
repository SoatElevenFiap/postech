using Soat.Eleven.FastFood.Domain.Entidades;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Domain.Gateways
{
    public interface IProdutoGateway
    {
        Task<Produto> AddAsync(Produto entity);
        Task<Produto?> GetByIdAsync(Guid id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<IEnumerable<Produto>> FindAsync(Expression<Func<Produto, bool>> predicate);
        Task UpdateAsync(Produto entity);
        Task DeleteAsync(Produto entity);
        Task<IEnumerable<Produto>> GetByCategoriaAsync(Guid categoriaId);
    }
}

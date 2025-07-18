using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Infra.Data;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Produto> _dbSet;

        public ProdutoGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Produto>();
        }

        public async Task<Produto> AddAsync(Produto entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Produto?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> FindAsync(Expression<Func<Produto, bool>> predicate)
        {
            return await _dbSet
                .Include(p => p.Categoria)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Produto entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaAsync(Guid categoriaId)
        {
            return await _dbSet
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

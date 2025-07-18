using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Infra.Data;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class CategoriaGateway : ICategoriaGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<CategoriaProduto> _dbSet;

        public CategoriaGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CategoriaProduto>();
        }

        public async Task<CategoriaProduto> AddAsync(CategoriaProduto entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CategoriaProduto?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<CategoriaProduto>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CategoriaProduto>> FindAsync(Expression<Func<CategoriaProduto, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(CategoriaProduto entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CategoriaProduto entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

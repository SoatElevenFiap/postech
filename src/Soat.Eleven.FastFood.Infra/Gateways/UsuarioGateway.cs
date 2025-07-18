using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class UsuarioGateway : IUsuarioGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Usuario> _dbSet;

        public UsuarioGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Usuario>();
        }

        public async Task<Usuario> AddAsync(Usuario entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Usuario?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.Cliente)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Usuario entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

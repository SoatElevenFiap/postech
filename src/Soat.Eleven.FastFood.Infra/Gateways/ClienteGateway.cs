using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class ClienteGateway : IClienteGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Cliente> _dbSet;

        public ClienteGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Cliente>();
        }

        public async Task<Cliente> AddAsync(Cliente entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Cliente?> GetByCpfAsync(string cpf)
        {
            return await _dbSet
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Cliente entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cliente entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

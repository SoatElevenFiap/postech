using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Infra.Data;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class PedidoGateway : IPedidoGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Pedido> _dbSet;

        public PedidoGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Pedido>();
        }

        public async Task<Pedido> AddAsync(Pedido entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Pedido?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> FindAsync(Expression<Func<Pedido, bool>> predicate)
        {
            return await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .Where(predicate)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Pedido entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pedido entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> SaveWithItemsAsync(Pedido pedido)
        {
            var existingPedido = await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .FirstOrDefaultAsync(p => p.Id == pedido.Id);

            if (existingPedido != null)
            {
                _context.Entry(existingPedido).CurrentValues.SetValues(pedido);
                
                // Atualizar itens
                existingPedido.Itens.Clear();
                foreach (var item in pedido.Itens)
                {
                    existingPedido.Itens.Add(item);
                }
            }
            else
            {
                await _dbSet.AddAsync(pedido);
            }

            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}

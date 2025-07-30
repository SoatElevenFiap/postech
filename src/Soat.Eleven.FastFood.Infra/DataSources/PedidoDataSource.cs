using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Infra.Data;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Adapter.Infra.DataSources
{
    public class PedidoDataSource : IPedidoDataSource
    {
        private readonly AppDbContext _context;
        private readonly DbSet<PedidoModel> _dbSet;

        public PedidoDataSource(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<PedidoModel>();
        }

        public async Task<Pedido> AddAsync(Pedido entity)
        {
            var model = Parse(entity);
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
            return Parse(model);
        }

        public async Task<Pedido?> GetByIdAsync(Guid id)
        {
            var result = await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == id);
            return result != null ? Parse(result) : null;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            var result = await _dbSet
                .Include(p => p.Itens)
                .Include(p => p.Pagamentos)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
            return result.Select(Parse);
        }

        public async Task<IEnumerable<Pedido>> FindAsync(Expression<Func<Pedido, bool>> predicate)
        {
            throw new NotImplementedException("This method is not implemented yet.");
            //return await _dbSet
            //    .Include(p => p.Itens)
            //    .Include(p => p.Pagamentos)
            //    .Where(predicate)
            //    .AsSplitQuery()
            //    .AsNoTracking()
            //    .ToListAsync();
        }

        public async Task UpdateAsync(Pedido entity)
        {
            var model = Parse(entity);
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pedido entity)
        {
            var model = Parse(entity);
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> SaveWithItemsAsync(Pedido pedido)
        {
            throw new NotImplementedException("This method is not implemented yet.");
            //var existingPedido = await _dbSet
            //    .Include(p => p.Itens)
            //    .Include(p => p.Pagamentos)
            //    .FirstOrDefaultAsync(p => p.Id == pedido.Id);

            //if (existingPedido != null)
            //{
            //    _context.Entry(existingPedido).CurrentValues.SetValues(pedido);

            //    // Atualizar itens
            //    existingPedido.Itens.Clear();
            //    foreach (var item in pedido.Itens)
            //    {
            //        existingPedido.Itens.Add(item);
            //    }
            //}
            //else
            //{
            //    await _dbSet.AddAsync(pedido);
            //}

            //await _context.SaveChangesAsync();
            //return pedido;
        }

        private static PedidoModel Parse(Pedido entity)
        {
            var model = new PedidoModel(entity.TokenAtendimentoId,
                                        entity.ClienteId,
                                        entity.Subtotal,
                                        entity.Desconto,
                                        entity.Total);
            return model;
        }

        private static Pedido Parse(PedidoModel model)
        {
            return new Pedido(model.TokenAtendimentoId,
                              model.ClienteId,
                              model.Subtotal,
                              model.Desconto,
                              model.Total);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<ProdutoModel> _dbSet;

        public ProdutoGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProdutoModel>();
        }

        public async Task AddAsync(Produto entity)
        {
            var model = Parse(entity);
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto?> GetByIdAsync(Guid id)
        {
            var result = await _dbSet
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(e => e.Id == id);
            return result != null ? Parse(result) : null;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            var result = await _dbSet
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
            return result.Select(Parse);
        }

        public async Task<IEnumerable<Produto>> FindAsync(Func<Produto, bool> predicate)
        {
            throw new NotImplementedException("This method is not implemented yet.");
            //return await _dbSet
            //    .Include(p => p.Categoria)
            //    .Where(predicate)
            //    .AsNoTracking()
            //    .ToListAsync();
        }

        public async Task UpdateAsync(Produto entity)
        {
            var model = Parse(entity);
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto entity)
        {
            var model = Parse(entity);
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaAsync(Guid categoriaId)
        {
            var result = await _dbSet
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .AsNoTracking()
                .ToListAsync();
            return result.Select(Parse);
        }

        private static ProdutoModel Parse(Produto entity)
        {
            var model = new ProdutoModel
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Preco = entity.Preco,
                CategoriaId = entity.CategoriaId,
                Ativo = entity.Ativo
            };
            return model;
        }

        private static Produto Parse(ProdutoModel model)
        {
            return new Produto
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Preco = model.Preco,
                CategoriaId = model.CategoriaId,
                Ativo = model.Ativo
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Infra.Data;
using System.Linq.Expressions;

namespace Soat.Eleven.FastFood.Infra.Gateways
{
    public class CategoriaGateway : ICategoriaGateway
    {
        private readonly AppDbContext _context;
        private readonly DbSet<CategoriaProdutoModel> _dbSet;

        public CategoriaGateway(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CategoriaProdutoModel>();
        }

        public async Task<CategoriaProduto> AddAsync(CategoriaProduto entity)
        {
            var model = Parse(entity);
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
            return Parse(model);
        }

        public async Task<CategoriaProduto?> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            return result != null ? Parse(result) : null;
        }

        public async Task<IEnumerable<CategoriaProduto>> GetAllAsync()
        {
            var result = await _dbSet.AsNoTracking().ToListAsync();
            return result.Select(Parse);
        }

        public async Task<IEnumerable<CategoriaProduto>> FindAsync(Func<CategoriaProduto, bool> predicate)
        {
            var result = await _dbSet
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            var entities = result.Select(Parse);

            if (predicate != null)
            {
                entities = entities.AsQueryable().Where(predicate);
            }

            return entities;
        }

        public async Task<CategoriaProduto> UpdateAsync(CategoriaProduto entity)
        {
            var model = Parse(entity);
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
            return Parse(model);
        }

        public async Task DeleteAsync(CategoriaProduto entity)
        {
            var model = Parse(entity);
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }

        private static CategoriaProdutoModel Parse(CategoriaProduto entity)
        {
            var model = new CategoriaProdutoModel
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Descricao = entity.Descricao,
                Ativo = entity.Ativo
            };
            return model;
        }

        private static CategoriaProduto Parse(CategoriaProdutoModel model)
        {
            return new CategoriaProduto
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Ativo = model.Ativo
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Adapter.Infra.DataSources
{
    public class UsuarioDataSource : IUsuarioDataSource
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UsuarioModel> _dbSet;

        public UsuarioDataSource(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<UsuarioModel>();
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _dbSet.FindAsync(id);

            return usuario != null ? Parse(usuario) : null;
        }

        public async Task<UsuarioDto?> GetByEmailAsync(string email)
        {
            var exist = await _dbSet
                .AsNoTracking()
                .Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return exist.Any() ? Parse(exist.First()) : null;
        }

        public async Task UpdatePasswordAsync(Guid id, string password)
        {
            var model = await _dbSet.FindAsync(id);

            if (model == null)
            {
                throw new KeyNotFoundException($"Usuario com Id {id} não encontrada.");
            }

            model.Senha = password;
            model.ModificadoEm = DateTime.UtcNow;
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }

        private static UsuarioDto Parse(UsuarioModel model)
        {
            return new UsuarioDto
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                Telefone = model.Telefone,
                Perfil = model.Perfil,
                CriadoEm = model.CriadoEm,
                ModificadoEm = model.ModificadoEm,
                Status = model.Status
            };
        }

        public async Task<UsuarioDto?> GetByEmailAndPassword(string email, string senha)
        {
            var usuario = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            return usuario != null ? Parse(usuario) : null;
        }
    }
}

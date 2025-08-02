using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Adapter.Infra.EntityModel;
using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Infra.Data;

namespace Soat.Eleven.FastFood.Adapter.Infra.DataSources
{
    public class ClienteDataSource : IClienteDataSource
    {
        private readonly AppDbContext _context;
        private readonly DbSet<ClienteModel> _dbSet;

        public ClienteDataSource(AppDbContext context, DbSet<ClienteModel> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public Task<UsuarioClienteResponseDto> AddAsync(CriarClienteRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioClienteResponseDto> GetCliente(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioClienteResponseDto> GetClienteByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioClienteResponseDto> UpdateAsync(CriarClienteRequestDto dto, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

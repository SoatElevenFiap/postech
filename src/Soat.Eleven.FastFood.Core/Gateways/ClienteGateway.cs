using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;

namespace Soat.Eleven.FastFood.Core.Gateways
{
    public class ClienteGateway
    {
        IClienteDataSource _clienteDataSource;

        public ClienteGateway(IClienteDataSource clienteDataSource)
        {
            _clienteDataSource = clienteDataSource;
        }

        public async Task<Cliente> CriarCliente(Cliente entity)
        {
            var dto = new CriarClienteRequestDto
            {
                Nome = entity.Nome,
                Email = entity.Email,
                Telefone = entity.Telefone,
                Cpf = entity.Cpf,
                DataDeNascimento = entity.DataDeNascimento
            };

            var novoClienteDto = await _clienteDataSource.AddAsync(dto);

            return new Cliente
            {
                Id = novoClienteDto.Id,
                Nome = novoClienteDto.Nome,
                Email = novoClienteDto.Email,
                Telefone = novoClienteDto.Telefone,
                Cpf = novoClienteDto.Cpf,
                DataDeNascimento = novoClienteDto.DataDeNascimento
            };
        }

        public Task<bool> ExistCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cliente>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> GetByUsuarioId(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}

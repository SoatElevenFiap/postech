using Soat.Eleven.FastFood.Core.Entities;
namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IClienteUseCase
{
    Task<Cliente> InserirCliente(Cliente cliente);
    Task<Cliente> AtualizarCliente(Cliente cliente, Guid usuarioId);
    Task<Cliente> GetCliente(Guid usuarioId);
    Task<Cliente> GetClienteByCPF(string cpf);
}

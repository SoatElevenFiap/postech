using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IClienteUseCase
{
    Task<string> InserirCliente(Cliente cliente, IJwtTokenService jwtTokenService);
    Task<Cliente> AtualizarCliente(Cliente cliente, IJwtTokenService jwtTokenService);
    Task<Cliente> GetCliente(IJwtTokenService jwtTokenService);
    Task<Cliente> GetClienteByCPF(string cpf);
}

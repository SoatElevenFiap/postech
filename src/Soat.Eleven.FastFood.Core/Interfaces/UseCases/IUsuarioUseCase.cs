using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IUsuarioUseCase
{
    Task<string> InserirCliente(Cliente cliente, IJwtTokenService jwtTokenService);
    Task<Cliente> AtualizarCliente(Cliente cliente, IJwtTokenService jwtTokenService);
    Task<Cliente> GetCliente(IJwtTokenService jwtTokenService);
    Task<Cliente> GetClienteByCPF(string cpf);
    Task<Administrador> InserirAdministrador(Administrador administrador);
    Task<Administrador> AtualizarAdministrador(Administrador administrador, IJwtTokenService jwtTokenService);
    Task<Administrador> GetAdministrador(IJwtTokenService jwtTokenService);
}

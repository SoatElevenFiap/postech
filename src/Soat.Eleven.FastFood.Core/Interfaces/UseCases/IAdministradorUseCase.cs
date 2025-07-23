using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IAdministradorUseCase
{
    Task<Administrador> InserirAdministrador(Administrador administrador);
    Task<Administrador> AtualizarAdministrador(Administrador administrador, IJwtTokenService jwtTokenService);
    Task<Administrador> GetAdministrador(IJwtTokenService jwtTokenService);
}

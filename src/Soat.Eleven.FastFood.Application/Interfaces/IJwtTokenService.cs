using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Usuario usuario);
}

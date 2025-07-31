using Soat.Eleven.FastFood.Application.Dtos;

namespace Soat.Eleven.FastFood.Application.Services;

public interface IJwtTokenService
{
    /// <summary>
    /// Gera JWT Token para usuário vindo de login: Administrador/Cliente
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns>JWT (string)</returns>
    string GenerateToken(UsuarioDto usuario);
    /// <summary>
    /// Gera JWT Token para usuário vindo identificação no atendimento por CPF
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns>JWT (string)</returns>
    string GenerateToken(UsuarioDto usuario, string tokenAtendimento);
    /// <summary>
    /// Gera JWT Token para usuário vindo sem identificação no atendimento
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns>JWT (string)</returns>
    string GenerateToken(string tokenAtendimento);
    Guid GetIdUsuario();
}

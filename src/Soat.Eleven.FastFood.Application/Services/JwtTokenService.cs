using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Microsoft.Extensions.Configuration;
using Soat.Eleven.FastFood.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Soat.Eleven.FastFood.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateToken(Usuario usuario)
    {
        var role = usuario.Perfil == PerfilUsuario.Administrador ? nameof(PolicyRole.AdminLogin) : nameof(PolicyRole.ClienteLogin);

        return GenerateToken([
            new (JwtRegisteredClaimNames.Name, usuario.Nome),
            new (JwtRegisteredClaimNames.Email, usuario.Email),
            new (JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new ("AccessType", role)
        ]);
    }

    public Guid GetIdUsuario()
    {
        var id = ReadToken(JwtRegisteredClaimNames.Sub);

        return Guid.Parse(id);
    }

    public string GetTokenAtendimento()
    {
        return ReadToken("TokenAtendimento");
    }

    private string ReadToken(string typeClaim)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.First() ?? throw new AuthenticationFailureException("Usuário não autenticado");

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token.Replace("Bearer ", string.Empty)) ?? throw new AuthenticationFailureException("Usuário não autenticado");

        return (jsonToken as JwtSecurityToken)!.Claims.First(x => x.Type == typeClaim).Value;
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["SecretKeyPassword"]!);
        var expirationDate = DateTime.UtcNow.AddHours(2);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateToken(Usuario usuario, string tokenAtendimento)
    {
        return GenerateToken([
            new (JwtRegisteredClaimNames.Name, usuario.Nome),
            new (JwtRegisteredClaimNames.Email, usuario.Email),
            new (JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new ("TokenAtendimento", tokenAtendimento),
            new ("AccessType", nameof(PolicyRole.ClienteIdentification))
        ]);
    }

    public string GenerateToken(string tokenAtendimento)
    {
        return GenerateToken([
            new ("TokenAtendimento", tokenAtendimento),
            new ("AccessType", nameof(PolicyRole.ClienteIdentification))
        ]);
    }
}

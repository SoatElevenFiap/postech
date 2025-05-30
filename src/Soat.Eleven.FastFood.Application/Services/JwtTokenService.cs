using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Microsoft.Extensions.Configuration;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var role = usuario.Perfil == PerfilUsuario.Administrador ? nameof(PolicyRole.AdminLogin) : nameof(PolicyRole.ClienteLogin);

        return GenerateToken([
            new (ClaimTypes.Name, usuario.Nome),
            new (ClaimTypes.Email, usuario.Email),
            new ("AccessType", role),
        ]);
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
}

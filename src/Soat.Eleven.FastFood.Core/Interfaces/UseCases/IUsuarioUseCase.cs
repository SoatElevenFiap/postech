using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IUsuarioUseCase
{
    Task<Usuario> AlterarSenha(string newPassword, string currentPassword, IJwtTokenService jwtTokenService);
}

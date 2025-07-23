using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IAuthUseCase
{
    Task<string> Login(AuthUsuarioRequestDto authUsuarioRequestDto, IJwtTokenService jwtTokenService, IPasswordService passwordService);
}

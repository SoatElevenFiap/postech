using Soat.Eleven.FastFood.Core.DTOs.Auth;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IAuthUseCase
{
    Task<Usuario> Login(AuthUsuarioRequestDto authUsuarioRequestDto, Guid UsuarioId);
}

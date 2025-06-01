using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;

namespace Soat.Eleven.FastFood.Application.Interfaces;

public interface IAuthService
{
    Task<ResultResponse> LoginUsuario(AuthUsuarioRequestDto request);
}

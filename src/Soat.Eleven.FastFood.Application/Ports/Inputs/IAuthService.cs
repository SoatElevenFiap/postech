using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs;

public interface IAuthService
{
    Task<ResultResponse> LoginUsuario(AuthUsuarioRequestDto request);
}

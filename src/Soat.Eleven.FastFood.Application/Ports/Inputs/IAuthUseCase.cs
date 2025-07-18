using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;

namespace Soat.Eleven.FastFood.Application.Ports.Inputs
{
    public interface IAuthUseCase
    {
        Task<ResultResponse> LoginUsuario(AuthUsuarioRequestDto request);
    }
}

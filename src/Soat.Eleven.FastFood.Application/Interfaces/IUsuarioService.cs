using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;

namespace Soat.Eleven.FastFood.Application.Interfaces;

public interface IUsuarioService
{
    Task<ResultResponse> InserirCliente(CriarClienteRequestDto request);
    Task<ResultResponse> AtualizarCliente(Guid usuarioId, AtualizarClienteDto request);
    Task<ResultResponse> GetCliente(Guid usuarioId);
}

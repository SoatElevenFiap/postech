using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

namespace Soat.Eleven.FastFood.Application.Interfaces;

public interface IUsuarioService
{
    Task<ResultResponse> InserirCliente(CriarClienteRequestDto request);
    Task<ResultResponse> AtualizarCliente(Guid usuarioId, AtualizarClienteRequestDto request);
    Task<ResultResponse> InserirAdministrador(CriarAdmRequestDto request);
    Task<ResultResponse> AtualizarAdministrador(Guid usuarioId, AtualizarAdmRequestDto request);
    Task<ResultResponse> GetUsuario(Guid usuarioId);
    Task<ResultResponse> AlterarSenha(Guid usuarioId, AtualizarSenhaRequestDto request);
    Task<ResultResponse> GetClientePorCpf(string cpf);

}

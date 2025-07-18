using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

namespace Soat.Eleven.FastFood.Application.Ports.Inputs
{
    public interface IUsuarioUseCase 
    {
        Task<ResultResponse> InserirCliente(CriarClienteRequestDto request);
        Task<ResultResponse> AtualizarCliente(AtualizarClienteRequestDto request);
        Task<ResultResponse> InserirAdministrador(CriarAdmRequestDto request);
        Task<ResultResponse> AtualizarAdministrador(AtualizarAdmRequestDto request);
        Task<ResultResponse> GetUsuario();
        Task<ResultResponse> AlterarSenha(AtualizarSenhaRequestDto request);
        Task<ResultResponse> GetClientePorCpf(string cpf);
    }
}

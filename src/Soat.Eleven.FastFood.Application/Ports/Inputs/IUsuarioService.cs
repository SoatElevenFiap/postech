using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs;

public interface IUsuarioService 
{
    Task<ResultResponse> InserirCliente(CriarClienteRequestDto request);
    Task<ResultResponse> AtualizarCliente(AtualizarClienteRequestDto request);
    Task<ResultResponse> InserirAdministrador(CriarAdmRequestDto request);
    Task<ResultResponse> AtualizarAdministrador(AtualizarAdmRequestDto request);
    Task<ResultResponse> GetUsuario();
    Task<ResultResponse> AlterarSenha(AtualizarSenhaRequestDto request);
    Task<ResultResponse> GetClientePorCpf(string cpf);

}

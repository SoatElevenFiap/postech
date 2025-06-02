using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Core.Application.Portas.Inputs;

public interface IUsuarioService<Usuario> where Usuario : IEntity
{
    Task<Usuario> InserirCliente(Usuario request);
    Task<Usuario> AtualizarCliente(Usuario request);
    Task<Usuario> InserirAdministrador(Usuario request);
    Task<Usuario> AtualizarAdministrador(Usuario request);
    Task<Usuario> GetUsuario();
    Task<Usuario> AlterarSenha(Usuario request);
    Task<Usuario> GetClientePorCpf(string cpf);

}

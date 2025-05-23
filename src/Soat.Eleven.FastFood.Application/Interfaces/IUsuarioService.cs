using Soat.Eleven.FastFood.Application.DTOs.Usuario.Request;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> InserirCliente(CriarClienteDto request);
    Task<Usuario> AtualizarCliente(Guid usuarioId, AtualizarClienteDto request);
}

using Soat.Eleven.FastFood.Core.Entities;
namespace Soat.Eleven.FastFood.Core.Interfaces.UseCases;

public interface IUsuarioUseCase
{
    Task<Usuario> AlterarSenha(string newPassword, string currentPassword, Guid UsuarioId);
}

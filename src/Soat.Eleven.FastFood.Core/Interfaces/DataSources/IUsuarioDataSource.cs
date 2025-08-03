using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
using Soat.Eleven.FastFood.Core.Entities;

namespace Soat.Eleven.FastFood.Core.Interfaces.DataSources;

public interface IUsuarioDataSource
{
    Task UpdatePasswordAsync(Guid id, string password);
    Task<UsuarioDto?> GetByIdAsync(Guid id);
    Task<UsuarioDto?> GetByEmailAndPassword(string email, string senha);
}

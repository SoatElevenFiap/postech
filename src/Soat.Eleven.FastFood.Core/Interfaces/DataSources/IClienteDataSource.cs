using Soat.Eleven.FastFood.Core.DTOs.Usuarios;
namespace Soat.Eleven.FastFood.Core.Interfaces.DataSources;

public interface IClienteDataSource
{
    Task<UsuarioClienteResponseDto> AddAsync(CriarClienteRequestDto dto);
    Task<UsuarioClienteResponseDto> UpdateAsync(CriarClienteRequestDto dto, Guid id);
    Task<UsuarioClienteResponseDto> GetCliente(Guid id);
    Task<UsuarioClienteResponseDto> GetClienteByCPF(string cpf);
}

using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public interface ITokenAtendimentoService
    {
        Task<TokenAtendimentoDTO> GerarToken(Guid? clienteId = default, string? cpf= default);
        TokenAtendimentoDTO RecuperarTokenAtendimento(Guid tokenId);
        Task<TokenAtendimentoDTO?> RecuperarTokenMaisNovoPorCpfAsync(string cpf);
        Task<TokenAtendimentoDTO> GerarToken(Cliente cliente);
    }
}

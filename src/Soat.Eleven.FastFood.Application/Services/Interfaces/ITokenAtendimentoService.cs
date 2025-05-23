using Soat.Eleven.FastFood.Application.DTOs;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public interface ITokenAtendimentoService
    {
        TokenAtendimentoDTO GerarToken(Guid? clienteId, string? cpf);
        TokenAtendimentoDTO RecuperarTokenAtendimento(Guid tokenId);
        Task<TokenAtendimentoDTO> PersistirTokenAtendimento(TokenAtendimentoDTO token);
        Task<TokenAtendimentoDTO?> RecuperarTokenMaisNovoPorCpfAsync(string cpf);
    }
}

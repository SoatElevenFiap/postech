using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Services.Interfaces
{
    public interface ITokenAtendimentoService
    {
        TokenAtendimento GerarToken(Guid? clienteId, string? cpf);
        TokenAtendimento RecuperarTokenAtendimento(Guid tokenId);
        Task<TokenAtendimento> PersistirTokenAtendimento(TokenAtendimento token);
    }
}

using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Entities = Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Core.Application.Mappers
{
    public class TokenAtendimentoMapper
    {
        public static Entities.TokenAtendimento MapToEntity(TokenAtendimentoDTO dto)
        {
            return new Entities.TokenAtendimento
            {
                TokenId = dto.TokenId,
                ClienteId = dto.ClienteId,
                Cpf = dto.Cpf,
                CriadoEm = dto.CriadoEm,
                //Cliente = dto.Cliente
            };
        }

        public static TokenAtendimentoDTO MapToDto(Entities.TokenAtendimento entity)
        {
            return new TokenAtendimentoDTO
            {
                TokenId = entity.TokenId,
                ClienteId = entity.ClienteId,
                Cpf = entity.Cpf,
                CriadoEm = entity.CriadoEm,
                //Cliente = entity.Cliente
            };
        }
    }
}

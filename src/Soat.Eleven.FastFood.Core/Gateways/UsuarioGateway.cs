using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;

namespace Soat.Eleven.FastFood.Core.Gateways
{
    public class UsuarioGateway
    {
        private readonly IUsuarioDataSource _usuarioDataSource;

        public UsuarioGateway(IUsuarioDataSource usuarioDataSource)
        {
            _usuarioDataSource = usuarioDataSource;
        }

        internal async Task<Usuario> ObterUsuarioPodId(Guid id)
        {
            var usuarioDto = await _usuarioDataSource.GetByIdAsync(id);

            if (usuarioDto == null)
                throw new KeyNotFoundException($"Usuário com Id {id} não encontrado.");

            return new Usuario
            {
                Id = usuarioDto.Id,
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha
            };
        }

        public async Task<Usuario?> ValidarLoginEObterUsuario(string email, string senha)
        {
            var dto = await _usuarioDataSource.GetByEmailAndPassword(email, senha);

            if (dto == null)
                return null;

            return new Usuario
            (
                dto.Id,
                dto.Nome,
                dto.Email,
                dto.Senha,
                dto.Telefone,
                dto.Perfil,
                dto.Status
            );
        }

        internal async Task AtualizarSenha(Guid id, string senha)
        {
            await _usuarioDataSource.UpdatePasswordAsync(id, senha);
        }

        internal async Task<bool> ExistEmail(string email)
        {
            throw new NotImplementedException();
        }

        internal async Task<Usuario> AddAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        internal async Task<Usuario> GetByIdAsync(Guid usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}

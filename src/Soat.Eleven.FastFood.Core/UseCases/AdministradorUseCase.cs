using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;

public class AdministradorUseCase : IAdministradorUseCase
{
    private readonly IAdministradorGateway _administradorGateway;

    public AdministradorUseCase(IAdministradorGateway administradorGateway)
    {
        _administradorGateway = administradorGateway;
    }

    public async Task<Administrador> InserirAdministrador(Administrador request)
    {
        var existeEmail = await _administradorGateway.ExistEmail(request.Email);

        if (existeEmail)
            throw new Exception("Usuário já existe");

        var administrador = await _administradorGateway.AddAsync(request);

        return administrador;
    }

    public async Task<Administrador> AtualizarAdministrador(Administrador request, Guid usuarioId)
    {
        var adminstrador = await _administradorGateway.GetByIdAsync(usuarioId);

        if (adminstrador is null)
            throw new Exception("Usuário não encontrado");

        if (request.Email != adminstrador.Email)
        {
            var existeEmail = await _administradorGateway.ExistEmail(request.Email);

            if (existeEmail)
                throw new Exception("Endereço de e-mail já utilizado");
        }

        adminstrador.Nome = request.Nome;
        adminstrador.Email = request.Email;
        adminstrador.Telefone = request.Telefone;

        var result = await _administradorGateway.AddAsync(adminstrador);

        return result;
    }
    public async Task<Administrador> GetAdministrador(Guid usuarioId)
    {
        var administrador = await _administradorGateway.GetByIdAsync(usuarioId);

        if (administrador is null)
            throw new ArgumentException("teste");

        return administrador;
    }
}

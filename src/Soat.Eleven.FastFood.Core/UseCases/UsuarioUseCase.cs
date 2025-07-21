using Soat.Eleven.FastFood.Core.Entities;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.Services;
using Soat.Eleven.FastFood.Core.Interfaces.UseCases;

namespace Soat.Eleven.FastFood.Core.UseCases;
// usecase retorna entidade ou lista
// na controller , converte para DTO (ou responseReturn) e retorna
public class UsuarioUseCase : IUsuarioUseCase
{
    private readonly IUsuarioGateway _usuarioGateway;

    public UsuarioUseCase(IUsuarioGateway usuarioGateway)
    {
        _usuarioGateway = usuarioGateway;
    }

    public async Task<string> InserirCliente(Cliente request, IJwtTokenService jwtTokenService)
    {
        var existeEmail = await _usuarioGateway.ExistEmail(request.Email);
        var existeCpf = await _usuarioGateway.ExistCpf(request.Cpf);

        if (existeEmail || existeCpf)
        {
            throw new Exception("Usuário já existe");
        }

        var result = await _usuarioGateway.SaveCliente(request);

        //var tokenAtendimento = await _tokenAtendimentoUseCase.GerarToken(usuario.Cliente);

        var jwtToken = jwtTokenService.GenerateToken(result, string.Empty);

        return jwtToken;
    }

    public async Task<Cliente> AtualizarCliente(Cliente request, IJwtTokenService jwtTokenService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var cliente = await _usuarioGateway.GetClienteByUsuarioId(usuarioId);

        if (cliente is null)
            throw new Exception("Usuário não encontrado");

        if (request.Email != cliente.Email)
        {
            var existeEmail = await _usuarioGateway.ExistEmail(request.Email);

            if (existeEmail)
                throw new Exception("Endereço de e-mail já utilizado");
        }

        if (request.Cpf != cliente.Cpf)
        {
            var existeCpf = await _usuarioGateway.ExistCpf(request.Cpf);

            if (existeCpf)
                throw new Exception("Já existe um usuário com este CPF");
        }

        cliente.Nome = request.Nome;
        cliente.Email = request.Email;
        cliente.Telefone = request.Telefone;
        cliente.Cpf = request.Cpf;
        cliente.DataDeNascimento = request.DataDeNascimento;

        var result = await _usuarioGateway.SaveCliente(cliente);

        return result;
    }

    public async Task<Administrador> InserirAdministrador(Administrador request)
    {
        var existeEmail = await _usuarioGateway.ExistEmail(request.Email);

        if (existeEmail)
            throw new Exception("Usuário já existe");

        var administrador = await _usuarioGateway.SaveAdministrador(request);

        return administrador;
    }

    public async Task<Administrador> AtualizarAdministrador(Administrador request, IJwtTokenService jwtTokenService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var adminstrador = await _usuarioGateway.GetAdminstradorByUsuarioId(usuarioId);

        if (adminstrador is null)
            throw new Exception("Usuário não encontrado");

        if (request.Email != adminstrador.Email)
        {
            var existeEmail = await _usuarioGateway.ExistEmail(request.Email);

            if (existeEmail)
                throw new Exception("Endereço de e-mail já utilizado");
        }

        adminstrador.Nome = request.Nome;
        adminstrador.Email = request.Email;
        adminstrador.Telefone = request.Telefone;

        var result = await _usuarioGateway.SaveAdministrador(adminstrador);

        return result;
    }

    public async Task<Cliente> GetCliente(IJwtTokenService jwtTokenService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var cliente = await _usuarioGateway.GetClienteByUsuarioId(usuarioId);

        return cliente;
    }

    public async Task<Administrador> GetAdministrador(IJwtTokenService jwtTokenService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var administrador = await _usuarioGateway.GetAdminstradorByUsuarioId(usuarioId);

        return administrador;
    }

    public async Task<Usuario> AlterarSenha(string newPassword, string currentPassword, IJwtTokenService jwtTokenService, IPasswordService passwordService)
    {
        var usuarioId = jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetUsuarioById(usuarioId);

        if (usuario is null)
            throw new Exception("Usuário não encontrado");

        if (passwordService.Generate(currentPassword) != usuario.Senha)
            throw new Exception("Senha atual está incorreta");

        usuario.Senha = passwordService.Generate(newPassword);

        var result = await _usuarioGateway.Save(usuario);

        return result;
    }

    public async Task<Cliente> GetClienteByCPF(string cpf)
    {
        var cliente = await _usuarioGateway.GetClienteByCPF(cpf);

        if (cliente is null)
        {
            throw new Exception("Cliente não encontrado");
        }

        return cliente;
    }
}

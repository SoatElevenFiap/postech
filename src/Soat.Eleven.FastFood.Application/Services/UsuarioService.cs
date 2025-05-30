using FluentValidation;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services;

public class UsuarioService : BaseService<Usuario>, IUsuarioService
{
    private readonly IRepository<Cliente> _clienteRepositorio;
    private readonly IRepository<Usuario> _usuarioRepositorio;

    public UsuarioService(IValidator<Usuario> validator,
                          ILogger<Usuario> logger,
                          IRepository<Cliente> clienteRepositorio,
                          IRepository<Usuario> usuarioRepositorio) : base(validator, logger)
    {
        _clienteRepositorio = clienteRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<ResultResponse> InserirCliente(CriarClienteRequestDto request)
    {
        var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
        var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

        if (existeEmail || existeCpf)
        {
            return SendError("Usuário já existe");
        }

        if (ValideRequest(request)) return SendError();

        var usuario = new Usuario(request.Nome,
                                  request.Email,
                                  PasswordService.Generate(request.Senha),
                                  request.Telefone,
                                  PerfilUsuario.Cliente);
        usuario.CriarCliente(request.Cpf, request.DataDeNascimento);

        return Send((UsuarioClienteResponseDto)await _usuarioRepositorio.AddAsync(usuario));
    }

    public async Task<ResultResponse> AtualizarCliente(Guid usuarioId, AtualizarClienteRequestDto request)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId, x => x.Cliente);

        if (usuario is null)
        {
            return SendError("Usuário não encontrado");
        }

        if (request.Email != usuario.Email)
        {
            var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
            
            if (existeEmail)
            {
                return SendError("Usuário já existe");
            }
        }

        if (request.Cpf != usuario.Cliente.Cpf)
        {
            var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

            if (existeCpf)
            {
                return SendError("Usuário já existe");
            }
        }

        if (ValideRequest(request)) return SendError();

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Telefone = request.Telefone;
        usuario.Cliente.Cpf = request.Cpf;
        usuario.Cliente.DataDeNascimento = request.DataDeNascimento;

        await _usuarioRepositorio.UpdateAsync(usuario);

        return Send((UsuarioClienteResponseDto)usuario);
    }

    public async Task<ResultResponse> InserirAdministrador(CriarAdmRequestDto request)
    {
        var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();

        if (existeEmail)
        {
            return SendError("Usuário já existe");
        }

        if (ValideRequest(request)) return SendError();

        var usuario = new Usuario(request.Nome,
                                  request.Email,
                                  PasswordService.Generate(request.Senha),
                                  request.Telefone,
                                  PerfilUsuario.Administrador);

        return Send((UsuarioAdmResponseDto)await _usuarioRepositorio.AddAsync(usuario));
    }

    public async Task<ResultResponse> AtualizarAdministrador(Guid usuarioId, AtualizarAdmRequestDto request)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId);

        if (usuario is null)
        {
            return SendError("Usuário não encontrado");
        }

        if (request.Email != usuario.Email)
        {
            var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();

            if (existeEmail)
            {
                return SendError("Usuário já existe");
            }
        }

        if (ValideRequest(request)) return SendError();

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Telefone = request.Telefone;

        await _usuarioRepositorio.UpdateAsync(usuario);

        return Send((UsuarioAdmResponseDto)usuario);
    }

    public async Task<ResultResponse> GetUsuario(Guid usuarioId)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId, u => u.Cliente);

        return Send(usuario is null ? usuario : (UsuarioClienteResponseDto)usuario);
    }

    public async Task<ResultResponse> AlterarSenha(Guid usuarioId, AtualizarSenhaRequestDto request)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId, u => u.Cliente);

        if (usuario is null)
        {
            return SendError("Usuário não encontrado");
        }

        var resultValidator = new AtualizarSenhaValidation().Validate(request);
        if (!resultValidator.IsValid) return SendError(resultValidator);

        if (PasswordService.Generate(request.CurrentPassword) != usuario.Senha)
        {
            return SendError("Senha atual está incorreta");
        }

        usuario.Senha = PasswordService.Generate(request.NewPassword);

        await _usuarioRepositorio.UpdateAsync(usuario);

        return Send("Senha alterada com sucesso!");
    }

    public async Task<ResultResponse> GetClientePorCpf(string cpf)
    {
        var cliente = await _clienteRepositorio.FindAsync(x => x.Cpf == cpf);

        if (!cliente.Any())
        {
            return SendError("Cliente não encontrado");
        }

        var usuario = await _usuarioRepositorio.GetByIdAsync(cliente.First().UsuarioId, u => u.Cliente);

        return Send(usuario is null ? usuario : (UsuarioClienteResponseDto)usuario);
    }
}

using FluentValidation;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.UseCases;

public class UsuarioUseCase : BaseService<Usuario>, IUsuarioUseCase
{
    private readonly IClienteGateway _clienteGateway;
    private readonly IUsuarioGateway _usuarioGateway;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ITokenAtendimentoUseCase _tokenAtendimentoUseCase;

    public UsuarioUseCase(IValidator<Usuario> validator,
                          ILogger<Usuario> logger,
                          IClienteGateway clienteGateway,
                          IUsuarioGateway usuarioGateway,
                          IJwtTokenService jwtTokenService,
                          ITokenAtendimentoUseCase tokenAtendimentoUseCase) : base(validator, logger)
    {
        _clienteGateway = clienteGateway;
        _usuarioGateway = usuarioGateway;
        _jwtTokenService = jwtTokenService;
        _tokenAtendimentoUseCase = tokenAtendimentoUseCase;
    }

    public async Task<ResultResponse> InserirCliente(CriarClienteRequestDto request)
    {
        var existeEmail = await _usuarioGateway.GetByEmailAsync(request.Email);
        var existeCpf = await _clienteGateway.GetByCpfAsync(request.Cpf);

        if (existeEmail != null || existeCpf != null)
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

        await _usuarioGateway.AddAsync(usuario);

        var tokenAtendimento = await _tokenAtendimentoUseCase.GerarToken(usuario.Cliente);

        var jwtToken = _jwtTokenService.GenerateToken(usuario, tokenAtendimento.TokenId.ToString());

        return Send(jwtToken);
    }

    public async Task<ResultResponse> AtualizarCliente(AtualizarClienteRequestDto request)
    {
        var usuarioId = _jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        if (usuario is null)
            return SendError("Usuário não encontrado");

        if (request.Email != usuario.Email)
        {
            var existeEmail = await _usuarioGateway.GetByEmailAsync(request.Email);

            if (existeEmail != null)
                return SendError("Endereço de e-mail já utilizado");
        }

        if (request.Cpf != usuario.Cliente.Cpf)
        {
            var existeCpf = await _clienteGateway.GetByCpfAsync(request.Cpf);

            if (existeCpf != null)
                return SendError("Já existe um usuário com este CPF");
        }

        if (ValideRequest(request)) return SendError();

        usuario.Nome = request.Nome;
        usuario.Telefone = request.Telefone;
        usuario.Cliente.Cpf = request.Cpf;
        usuario.Cliente.DataDeNascimento = request.DataDeNascimento;

        await _usuarioGateway.UpdateAsync(usuario);

        return Send((UsuarioClienteResponseDto)usuario);
    }

    public async Task<ResultResponse> InserirAdministrador(CriarAdmRequestDto request)
    {
        var existeEmail = await _usuarioGateway.GetByEmailAsync(request.Email);

        if (existeEmail != null)
            return SendError("Usuário já existe");

        if (ValideRequest(request)) return SendError();

        var usuario = new Usuario(request.Nome,
                                  request.Email,
                                  PasswordService.Generate(request.Senha),
                                  request.Telefone,
                                  PerfilUsuario.Administrador);

        return Send((UsuarioAdmResponseDto)await _usuarioGateway.AddAsync(usuario));
    }

    public async Task<ResultResponse> AtualizarAdministrador(AtualizarAdmRequestDto request)
    {
        var usuarioId = _jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        if (usuario is null)
            return SendError("Usuário não encontrado");

        if (request.Email != usuario.Email)
        {
            var existeEmail = await _usuarioGateway.GetByEmailAsync(request.Email);

            if (existeEmail != null)
                return SendError("Endereço de e-mail já utilizado");
        }

        if (ValideRequest(request)) return SendError();

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Telefone = request.Telefone;

        await _usuarioGateway.UpdateAsync(usuario);

        return Send((UsuarioAdmResponseDto)usuario);
    }

    public async Task<ResultResponse> GetUsuario()
    {
        var usuarioId = _jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        return Send(usuario is null ? usuario : (UsuarioResponseDto)usuario);
    }

    public async Task<ResultResponse> AlterarSenha(AtualizarSenhaRequestDto request)
    {
        var usuarioId = _jwtTokenService.GetIdUsuario();
        var usuario = await _usuarioGateway.GetByIdAsync(usuarioId);

        if (usuario is null)
            return SendError("Usuário não encontrado");

        var resultValidator = new AtualizarSenhaValidation().Validate(request);
        if (!resultValidator.IsValid) return SendError(resultValidator);

        if (PasswordService.Generate(request.CurrentPassword) != usuario.Senha)
            return SendError("Senha atual está incorreta");

        usuario.Senha = PasswordService.Generate(request.NewPassword);

        await _usuarioGateway.UpdateAsync(usuario);

        return Send("Senha alterada com sucesso!");
    }

    public async Task<ResultResponse> GetClientePorCpf(string cpf)
    {
        var cliente = await _clienteGateway.GetByCpfAsync(cpf);

        if (cliente == null)
        {
            return SendError("Cliente não encontrado");
        }

        var usuario = await _usuarioGateway.GetByIdAsync(cliente.UsuarioId);

        return Send(usuario is null ? usuario : (UsuarioClienteResponseDto)usuario);
    }
}

using FluentValidation;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Application.Services;
using Soat.Eleven.FastFood.Application.Ports.Inputs;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Gateways;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.UseCases;

public class AuthUseCase : BaseService<Usuario>, IAuthUseCase
{
    private readonly IUsuarioGateway _usuarioGateway;
    private readonly IJwtTokenService _jwtTokenService;
    
    public AuthUseCase(IValidator<Usuario> validator,
                       ILogger<Usuario> logger,
                       IUsuarioGateway usuarioGateway,
                       IJwtTokenService jwtTokenService) : base(validator, logger)
    {
        _usuarioGateway = usuarioGateway;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ResultResponse> LoginUsuario(AuthUsuarioRequestDto request)
    {
        var validateResult = new AuthUsuarioValidation().Validate(request);
        if (!validateResult.IsValid)
            return SendError(validateResult);

        var usuario = await _usuarioGateway.GetByEmailAsync(request.Email);

        if (usuario is null)
            return SendError("E-mail e/ou Senha estão incorretos");

        if (!PasswordService.Equal(request.Senha, usuario.Senha))
            return SendError("E-mail e/ou Senha estão incorretos");

        var token = _jwtTokenService.GenerateToken(usuario);

        return Send(token);
    }
}

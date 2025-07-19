using FluentValidation;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;
using Soat.Eleven.FastFood.Application.Validations.Usuarios;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Application.Services;

public class AuthService : BaseService<Usuario>, IAuthService
{
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly IJwtTokenService _jwtTokenService;
    public AuthService(IValidator<Usuario> validator,
                                 ILogger<Usuario> logger,
                                 IRepository<Usuario> usuarioRepository,
                                 IJwtTokenService jwtTokenService) : base(validator, logger)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<ResultResponse> LoginUsuario(AuthUsuarioRequestDto request)
    {
        var validateResult = new AuthUsuarioValidation().Validate(request);
        if (!validateResult.IsValid)
            return SendError(validateResult);

        var usuario = (await _usuarioRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();

        if (usuario is null)
            return SendError("E-mail e/ou Senha estão incorretos");

        if (!PasswordService.Equal(request.Senha, usuario.Senha))
            return SendError("E-mail e/ou Senha estão incorretos");

        var token = _jwtTokenService.GenerateToken(usuario);

        return Send(token);
    }
}

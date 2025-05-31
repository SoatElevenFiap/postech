using FluentValidation;
using Soat.Eleven.FastFood.Application.DTOs.TokenAtendimento;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class AuthUsuarioValidation : AbstractValidator<AuthUsuarioRequestDto>
{
    public AuthUsuarioValidation()
    {
        RuleFor(a => a.Email).NotEmpty().EmailAddress();

        RuleFor(a => a.Senha).SetValidator(new PasswordValidation());
    }
}

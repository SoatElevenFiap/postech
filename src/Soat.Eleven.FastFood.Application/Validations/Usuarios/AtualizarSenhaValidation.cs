using FluentValidation;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class AtualizarSenhaValidation : AbstractValidator<AtualizarSenhaRequestDto>
{
    public AtualizarSenhaValidation()
    {
        RuleFor(c => c.NewPassword)
            .NotEqual(c => c.CurrentPassword);

        RuleFor(c => c.CurrentPassword).SetValidator(new PasswordValidation("CurrentPassword"));

        RuleFor(c => c.NewPassword).SetValidator(new PasswordValidation("NewPassword"));
    }
}

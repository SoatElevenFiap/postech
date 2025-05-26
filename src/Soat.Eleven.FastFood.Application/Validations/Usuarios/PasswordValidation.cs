using FluentValidation;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class PasswordValidation : AbstractValidator<string>
{
    public PasswordValidation(string propertyName = "Senha")
    {
        RuleFor(x => x)
            .Matches("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*\\W+).{8,}$")
            .OverridePropertyName(propertyName);
    }
}

using FluentValidation;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class UsuarioValidation : AbstractValidator<Usuario>
{
    public UsuarioValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .Custom((value, context) =>
            {
                var name = value.Split(" ");

                if (name.Length == 1)
                    context.AddFailure("Usuário deve conter nome e sobrenome");
            });

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Senha).SetValidator(new PasswordValidation());

        When(c => c.Cliente is not null, () =>
        {
            RuleFor(c => c.Cliente.Cpf)
                .NotEmpty()
                .Length(11);

            RuleFor(c => c.Cliente.DataDeNascimento)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.Now);
        });
    }
}

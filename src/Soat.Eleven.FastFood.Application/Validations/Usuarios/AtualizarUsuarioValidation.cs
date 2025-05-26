using FluentValidation;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class AtualizarUsuarioValidation : AbstractValidator<Usuario>
{
    public AtualizarUsuarioValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

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

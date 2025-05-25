using FluentValidation;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;

namespace Soat.Eleven.FastFood.Application.Validations.Usuarios;

public class CriarClienteValidation : AbstractValidator<CriarClienteRequestDto>
{
    public CriarClienteValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty();

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(c => c.Cpf)
            .NotEmpty()
            .Length(11);
    }
}

using FluentValidation;
using FluentValidation.Results;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Application.Services;

public abstract class BaseService<T>
{
    private readonly IValidator<T> _validator;
    protected ValidationResult ValidationResult => new();

    protected void AddError(string error) =>
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));

    protected ResultResponse Send(object data)
    {
        if (ValidationResult.Errors.Count != 0)
            return ResultResponse.SendError(ValidationResult);


        return ResultResponse.SendSuccess(data);
    }

    protected ResultResponse SendError()
    {
        return ResultResponse.SendError(ValidationResult);
    }

    protected bool ValideRequest(T data)
    {
        var result = _validator.Validate(data);
        ValidationResult.Errors = result.Errors;

        return !result.IsValid;
    }
}

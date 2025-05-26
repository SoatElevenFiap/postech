using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Soat.Eleven.FastFood.Application.Configuration;

namespace Soat.Eleven.FastFood.Application.Services;

public abstract class BaseService<T>
{
    private readonly IValidator<T> _validator;
    protected readonly ILogger<T> _logger;
    private readonly ValidationResult _validationResult;

    protected BaseService(IValidator<T> validator, ILogger<T> logger)
    {
        _validator = validator;
        _logger = logger;
        _validationResult = new ValidationResult();
    }

    protected ResultResponse Send(object? data)
    {
        if (_validationResult.Errors.Count != 0)
            return ResultResponse.SendError(_validationResult);


        return ResultResponse.SendSuccess(data);
    }

    protected ResultResponse SendError()
    {
        return ResultResponse.SendError(_validationResult);
    }

    protected ResultResponse SendError(ValidationResult validationResult)
    {
        return ResultResponse.SendError(validationResult);
    }

    protected ResultResponse SendError(string message)
    {
        _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        return ResultResponse.SendError(_validationResult);
    }

    protected bool ValideRequest(T data)
    {
        var result = _validator.Validate(data);
        _validationResult.Errors.AddRange(result.Errors);

        return !result.IsValid;
    }
}

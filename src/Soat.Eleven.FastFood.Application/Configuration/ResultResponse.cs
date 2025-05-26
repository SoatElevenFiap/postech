using FluentValidation.Results;

namespace Soat.Eleven.FastFood.Application.Configuration;

public class ResultResponse
{
    private ResultResponse()
    {
    }

    public bool Success { get; set; }
    public IList<ValidationFailure> Errors { get; set; }
    public object? Data { get; set; }

    public static ResultResponse SendSuccess(object? data)
    {
        return new ResultResponse
        {
            Success = true,
            Data = data
        };
    }

    public static ResultResponse SendError(ValidationResult error)
    {
        return new ResultResponse
        {
            Success = false,
            Errors = error.Errors
        };
    }
}

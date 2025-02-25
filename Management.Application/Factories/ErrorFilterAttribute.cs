using FluentValidation.Results;
using Management.Common.Erros;

namespace Management.Factories;

public static class ErrorFilterAttribute
{
    public static ErrorResponse CreateObjectError(ValidationResult errorResult)
    {
        var errorResponse = new ErrorResponse
        {
            Code = "400",
            Message = "Validation error: missing required field",
            Details = errorResult.Errors
                .Select(error =>
                    new ErrorDetail
                    {
                        Field = error.PropertyName,
                        Error = error.ErrorMessage,
                }).ToList()
        };
        return errorResponse;
    }
}
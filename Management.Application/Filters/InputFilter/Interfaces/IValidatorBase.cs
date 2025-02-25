using FluentValidation.Results;

namespace Management.Filters.InputFilter.Interfaces;

public interface IValidatorBase<in T>
{
    public ValidationResult Validate(T type);
}
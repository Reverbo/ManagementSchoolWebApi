using FluentValidation.Results;
using Management.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Management.Filters.InputFilter.Interfaces;

namespace Management.Filters.InputFilter;

public class ObjectRequestFilterAttribute : ActionFilterAttribute
{
    private IServiceProvider _serviceProvider;

    public ObjectRequestFilterAttribute(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var requestIs = context.HttpContext.Request.Method;
        var objectType = context.ActionArguments.Values.First();

        if (requestIs != "POST" && requestIs != "PUT")
        {
            base.OnActionExecuting(context);
            return;
        }

        if (objectType == null)
        {
            base.OnActionExecuting(context);
            return;
        }

        var validatorType = typeof(IValidatorBase<>).MakeGenericType(objectType.GetType());

        var validator = _serviceProvider.GetService(validatorType);

        if (validator == null)
        {
            base.OnActionExecuting(context);
            return;
        }

        var method = validatorType.GetMethod("Validate");

        if (method == null)
        {
            base.OnActionExecuting(context);
            return;
        }

        var validationResult = method.Invoke(validator, new[] { objectType }) as ValidationResult;

        if (validationResult != null && !validationResult.IsValid)
        {
            var errorResponse = ErrorFilterAttribute.CreateObjectError(validationResult);
            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }

        base.OnActionExecuting(context);
    }
}
using FluentResults;
using FluentValidation;
using MediatR;
using StaffHub.Application.Common.Errors;

namespace StaffHub.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(r => r.Errors)
            .Where(e => e is not null)
            .Select(e => (IError)new Error(e.ErrorMessage)
                .WithMetadata("ErrorType", AppErrors.ValidationType)
                .WithMetadata("PropertyName", e.PropertyName)
                .WithMetadata("ErrorCode", e.ErrorCode))
            .ToList();

        if (errors.Count == 0)
            return await next();

        return CreateFailureResponse(errors);
    }

    private static TResponse CreateFailureResponse(List<IError> errors)
    {
        if (typeof(TResponse) == typeof(Result))
            return (TResponse)(object)Result.Fail(errors);

        if (typeof(TResponse).IsGenericType
            && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = typeof(TResponse).GetGenericArguments()[0];
            var method = typeof(Result)
                .GetMethods()
                .Single(m =>
                    m.Name == nameof(Result.Fail)
                    && m.IsGenericMethodDefinition
                    && m.GetParameters().Length == 1
                    && m.GetParameters()[0].ParameterType == typeof(IEnumerable<IError>));

            return (TResponse)method.MakeGenericMethod(valueType).Invoke(null, [errors])!;
        }

        throw new InvalidOperationException(
            $"Validation failed, but {typeof(TResponse).Name} is not a FluentResults response.");
    }
}
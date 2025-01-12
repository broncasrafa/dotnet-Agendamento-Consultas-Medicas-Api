using System.Reflection;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using FluentValidation;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Behaviors.Pipelines;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{

    private static readonly Type _type = typeof(TResponse);
    private static readonly MethodInfo? _operationResultError;
    private readonly AbstractValidator<TRequest> _validator;

    static ValidationPipelineBehavior()
    {
        if (_type.IsGenericType)
            _operationResultError = ValidationPipelineBehaviorMeta._genericErrorMethod.MakeGenericMethod(_type.GetGenericArguments().First());
    }

    public ValidationPipelineBehavior(AbstractValidator<TRequest> validator)
        => _validator = validator;


    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validationResult = _validator.Validate(request);
        if (validationResult.IsValid)
            return next.Invoke();

        var errors = validationResult.Errors.GroupBy(err => err.PropertyName, err => err.ErrorMessage).ToDictionary(err => err.Key, err => err.Select(c => c));
        var validationError = new InputRequestDataInvalidException(errors);

        return _type == ValidationPipelineBehaviorMeta._typeOperationResult
            ? Task.FromResult((TResponse)Convert.ChangeType(Result.Error(validationError), _type))
            : Task.FromResult((TResponse)Convert.ChangeType(_operationResultError!.Invoke(null, new object[] { validationError }), _type)!);
    }
}

internal static class ValidationPipelineBehaviorMeta
{
    internal static readonly Type _typeOperationResult = typeof(Result);
    internal static readonly MethodInfo _genericErrorMethod = _typeOperationResult!.GetMethods().First(m => m.Name == nameof(Result.Error) && m.IsGenericMethod);
}
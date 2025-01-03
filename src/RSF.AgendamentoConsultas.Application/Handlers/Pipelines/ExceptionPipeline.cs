using System.Reflection;
using Microsoft.Extensions.Logging;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Handlers.Pipelines;

public sealed class ExceptionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly MethodInfo? _operationResultError;
    private readonly Type _type = typeof(TResponse);
    private readonly Type _typeOperationResult = typeof(Result);
    private readonly ILogger<ExceptionPipeline<TRequest, TResponse>> _logger;

    public ExceptionPipeline(ILogger<ExceptionPipeline<TRequest, TResponse>> logger)
    {
        if (_type.IsGenericType)
        {
            _operationResultError = _typeOperationResult.GetMethods().First(m => m.Name == "Error" && m.IsGenericMethod);
            _operationResultError = _operationResultError.MakeGenericMethod(_type.GetGenericArguments().First());
        }

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var eventId = default(EventId);

        try
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogInformation(eventId, "Handling request of type {TypeName} with data {@Data}", typeof(TRequest).FullName, request);
            else
                _logger.LogInformation(eventId, "Handling request of type {TypeName}", typeof(TRequest).FullName);

            return await next.Invoke();
        }
        catch (Exception ex)
        {
            _logger.LogError(eventId, ex, "An error occurred while handling request of type {TypeName}", typeof(TRequest).FullName);
            return _type == _typeOperationResult
                ? (TResponse)Convert.ChangeType(Result.Error(ex), _type)
                : (TResponse)Convert.ChangeType(_operationResultError!.Invoke(null, new object[] { ex })!, _type)!;
        }
    }
}
using System.Net;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Shareable.Filters;
using MediatR;
using OperationResult;
using FluentValidation;


namespace RSF.AgendamentoConsultas.Api.Extensions;

internal static class EndpointExtension
{
    public static async Task<IResult> SendCommand<T>(this IMediator mediator, IRequest<Result<T>> request, Func<T, IResult>? result = null, CancellationToken cancellationToken = default)    
        => await mediator.Send(request, cancellationToken) switch
        {
            (true, var response, _) => result is not null ? result(response!) : TypedResults.Ok(new ApiResponse<T>(response!)),
            var (_, _, error) => HandleError(error!)
        };

    public static async Task<IResult> SendCommand(this IMediator mediator, IRequest<Result> request, Func<IResult>? result = null, CancellationToken cancellationToken = default)
        => await mediator.Send(request, cancellationToken) switch
        {
            (true, _) => result is not null ? result() : TypedResults.Ok(),
            var (_, error) => HandleError(error!)
        };



    private static IResult HandleError(Exception exception)
    {
        var httpContextAccessor = new HttpContextAccessor();
        var context = httpContextAccessor.HttpContext;

        return exception switch
        {
            ValidationException err => TypedResults.BadRequest(ValidationProblemDetails(err.Errors.Select(e => e.ErrorMessage).ToList(), context)),
            InputRequestDataInvalidException err => TypedResults.BadRequest(ValidationProblemDetails(err.Errors, context)),
            BaseException err => TypedResults.Problem(ProblemDetails(err.Message, err.StatusCode, context)),
            _ => TypedResults.Problem(ProblemDetails("An internal server error occurred.", HttpStatusCode.InternalServerError, context)),
        };
    }



    //private static IResult InternalServerError(HttpContext? context, string? message = null)
    //{
    //    var problemDetails = new ValidationProblemDetails
    //    {
    //        Instance = context?.Request?.HttpContext?.Request?.Path,
    //        Status = StatusCodes.Status500InternalServerError,
    //        Title = message ?? "An internal server error occurred."
    //    };

    //    return Results.Json(problemDetails, statusCode: StatusCodes.Status500InternalServerError);
    //}

    private static ValidationProblemDetails ValidationProblemDetails(IEnumerable<string> errors, HttpContext? context)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Instance = context?.Request?.HttpContext?.Request?.Path,
            Status = StatusCodes.Status400BadRequest,
            Title = "Ocorreram um ou mais erros de validação.",
            Errors = errors.ToList()
        };

        return problemDetails;
    }

    private static Microsoft.AspNetCore.Mvc.ProblemDetails ProblemDetails(string message, HttpStatusCode statusCode, HttpContext? context)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Instance = context?.Request?.HttpContext?.Request?.Path,
            Status = (int)statusCode,
            Title = message
        };

        return problemDetails;
    }
}
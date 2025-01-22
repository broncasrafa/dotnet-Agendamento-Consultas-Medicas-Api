using System.Diagnostics.CodeAnalysis;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class EntityValidationException : Exception
{
    public IReadOnlyCollection<ValidationError>? Errors { get; }

    public EntityValidationException(string message, IReadOnlyCollection<ValidationError>? errors = null) : base(message)
    {
        Errors = errors;
    }
}
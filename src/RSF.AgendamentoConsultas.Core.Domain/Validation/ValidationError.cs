using System.Diagnostics.CodeAnalysis;


namespace RSF.AgendamentoConsultas.Core.Domain.Validation;

[ExcludeFromCodeCoverage]
public record ValidationError(string Message);
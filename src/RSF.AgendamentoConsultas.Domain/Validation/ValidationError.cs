using System.Diagnostics.CodeAnalysis;


namespace RSF.AgendamentoConsultas.Domain.Validation;

[ExcludeFromCodeCoverage]
public record ValidationError(string Message);
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetAll;

public class SelectEstadoRequestValidator : AbstractValidator<SelectEstadoRequest>
{
    public SelectEstadoRequestValidator()
    {
    }
}
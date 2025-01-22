using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.GetAll;

public class SelectEstadoRequestValidator : AbstractValidator<SelectEstadoRequest>
{
    public SelectEstadoRequestValidator()
    {
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public class SelectEstadoRequestValidator : AbstractValidator<SelectEstadoRequest>
{
    public SelectEstadoRequestValidator()
    {
    }
}
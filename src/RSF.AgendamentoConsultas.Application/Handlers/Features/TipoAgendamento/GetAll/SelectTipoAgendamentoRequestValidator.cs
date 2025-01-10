using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.GetAll;

public class SelectTipoAgendamentoRequestValidator : AbstractValidator<SelectTipoAgendamentoRequest>
{
    public SelectTipoAgendamentoRequestValidator()
    {
    }
}
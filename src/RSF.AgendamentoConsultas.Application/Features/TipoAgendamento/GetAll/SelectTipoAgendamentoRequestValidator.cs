using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.TipoAgendamento.GetAll;

public class SelectTipoAgendamentoRequestValidator : AbstractValidator<SelectTipoAgendamentoRequest>
{
    public SelectTipoAgendamentoRequestValidator()
    {
    }
}
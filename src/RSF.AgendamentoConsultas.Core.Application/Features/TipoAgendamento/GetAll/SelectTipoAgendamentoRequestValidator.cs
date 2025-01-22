using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.GetAll;

public class SelectTipoAgendamentoRequestValidator : AbstractValidator<SelectTipoAgendamentoRequest>
{
    public SelectTipoAgendamentoRequestValidator()
    {
    }
}
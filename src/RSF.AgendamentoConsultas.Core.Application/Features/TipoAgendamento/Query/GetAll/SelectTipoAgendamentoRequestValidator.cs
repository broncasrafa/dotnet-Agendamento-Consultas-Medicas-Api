using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Query.GetAll;

public class SelectTipoAgendamentoRequestValidator : AbstractValidator<SelectTipoAgendamentoRequest>
{
    public SelectTipoAgendamentoRequestValidator()
    {
    }
}
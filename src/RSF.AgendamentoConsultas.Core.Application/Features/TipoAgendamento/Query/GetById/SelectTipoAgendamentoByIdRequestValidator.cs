using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Query.GetById;

public class SelectTipoAgendamentoByIdRequestValidator : AbstractValidator<SelectTipoAgendamentoByIdRequest>
{
    public SelectTipoAgendamentoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Tipo de Agendamento");
    }
}
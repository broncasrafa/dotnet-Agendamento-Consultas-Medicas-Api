using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Query.GetAgendamentoById;

public class SelectAgendamentoByIdRequestValidator : AbstractValidator<SelectAgendamentoByIdRequest>
{
    public SelectAgendamentoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Agendamento");
    }
}
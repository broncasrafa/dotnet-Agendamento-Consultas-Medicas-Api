using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;

public class CancelAgendamentoByEspecialistaRequestValidator : AbstractValidator<CancelAgendamentoByEspecialistaRequest>
{
    public CancelAgendamentoByEspecialistaRequestValidator()
    {
        RuleFor(x => x.AgendamentoId).IdValidators("Agendamento");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;

public class ConfirmAgendamentoByEspecialistaRequestValidator : AbstractValidator<ConfirmAgendamentoByEspecialistaRequest>
{
    public ConfirmAgendamentoByEspecialistaRequestValidator()
    {
        RuleFor(x => x.AgendamentoId).IdValidators("Agendamento");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
    }
}
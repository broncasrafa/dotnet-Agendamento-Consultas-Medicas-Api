using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;

public class ConfirmAgendamentoByPacienteRequestValidator : AbstractValidator<ConfirmAgendamentoByPacienteRequest>
{
    public ConfirmAgendamentoByPacienteRequestValidator()
    {
        RuleFor(x => x.AgendamentoId).IdValidators("Agendamento");
        RuleFor(x => x.PacienteId).IdValidators("PacienteId");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;

public class CancelAgendamentoByPacienteRequestValidator : AbstractValidator<CancelAgendamentoByPacienteRequest>
{
    public CancelAgendamentoByPacienteRequestValidator()
    {
        RuleFor(x => x.AgendamentoId).IdValidators("Agendamento");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.DependenteId).IdValidators("Dependente", c => c.DependenteId.HasValue);
        RuleFor(c => c.MotivoCancelamento).NotNullOrEmptyValidators("Motivo do cancelamento", minLength: 5);
    }
}
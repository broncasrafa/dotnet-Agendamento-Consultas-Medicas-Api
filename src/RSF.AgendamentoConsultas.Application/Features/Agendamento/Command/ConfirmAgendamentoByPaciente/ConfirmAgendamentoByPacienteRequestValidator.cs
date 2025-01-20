using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;

public class ConfirmAgendamentoByPacienteRequestValidator : AbstractValidator<ConfirmAgendamentoByPacienteRequest>
{
    public ConfirmAgendamentoByPacienteRequestValidator()
    {
        RuleFor(x => x.AgendamentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Agendamento deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
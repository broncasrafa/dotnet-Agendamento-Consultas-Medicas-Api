using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;

public class CancelAgendamentoByPacienteRequestValidator : AbstractValidator<CancelAgendamentoByPacienteRequest>
{
    public CancelAgendamentoByPacienteRequestValidator()
    {
        RuleFor(x => x.AgendamentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Agendamento deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.DependenteId)
            .GreaterThan(0).When(c => c.DependenteId.HasValue)
            .WithMessage("O ID do Dependente deve ser maior que 0");

        RuleFor(c => c.MotivoCancelamento)
            .NotEmpty().WithMessage("O Motivo do cancelamento é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O Motivo do cancelamento deve ter pelo menos 5 caracteres");
    }
}
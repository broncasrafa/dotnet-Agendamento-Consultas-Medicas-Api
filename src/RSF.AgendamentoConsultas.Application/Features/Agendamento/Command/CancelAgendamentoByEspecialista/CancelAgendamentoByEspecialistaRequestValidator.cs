using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;

public class CancelAgendamentoByEspecialistaRequestValidator : AbstractValidator<CancelAgendamentoByEspecialistaRequest>
{
    public CancelAgendamentoByEspecialistaRequestValidator()
    {
        RuleFor(x => x.AgendamentoId)
        .GreaterThan(0)
        .WithMessage("O ID do Agendamento deve ser maior que 0");

        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
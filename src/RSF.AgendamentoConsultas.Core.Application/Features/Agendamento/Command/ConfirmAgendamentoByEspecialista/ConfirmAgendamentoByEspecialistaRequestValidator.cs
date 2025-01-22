using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;

public class ConfirmAgendamentoByEspecialistaRequestValidator : AbstractValidator<ConfirmAgendamentoByEspecialistaRequest>
{
    public ConfirmAgendamentoByEspecialistaRequestValidator()
    {
        RuleFor(x => x.AgendamentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Agendamento deve ser maior que 0");

        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
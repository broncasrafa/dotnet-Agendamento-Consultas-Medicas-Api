using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;

public class DeleteLocalAtendimentoRequestValidator : AbstractValidator<DeleteLocalAtendimentoRequest>
{
    public DeleteLocalAtendimentoRequestValidator()
    {
        RuleFor(c => c.EspecialistaId)
        .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.LocalAtendimentoId)
            .GreaterThan(0).WithMessage("O ID do Local de Atendimento deve ser maior que 0");
    }
}
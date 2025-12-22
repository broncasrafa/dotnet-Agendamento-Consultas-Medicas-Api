using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;

public class DeleteLocalAtendimentoRequestValidator : AbstractValidator<DeleteLocalAtendimentoRequest>
{
    public DeleteLocalAtendimentoRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.LocalAtendimentoId).IdValidators("Local de Atendimento");
    }
}
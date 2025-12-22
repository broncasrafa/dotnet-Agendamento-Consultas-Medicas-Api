using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteEspecialidade;

public class DeleteEspecialidadeRequestValidator : AbstractValidator<DeleteEspecialidadeRequest>
{
    public DeleteEspecialidadeRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.EspecialidadeId).IdValidators("Especialidade");
    }
}
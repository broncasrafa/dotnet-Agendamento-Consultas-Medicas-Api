using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialidade;

public class UpdateEspecialidadeRequestValidator : AbstractValidator<UpdateEspecialidadeRequest>
{
    public UpdateEspecialidadeRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("registro");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.EspecialidadeId).IdValidators("Especialidade");
        RuleFor(c => c.TipoEspecialidade)
            .NotNullOrEmptyValidators("Tipo de Especialidade")
            .Must(tipo =>
                    tipo.Equals("Principal", StringComparison.InvariantCultureIgnoreCase) ||
                    tipo.Equals("SubEspecialidade", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("O Tipo de Especialidade deve ser 'Principal' ou 'SubEspecialidade'");
    }
}
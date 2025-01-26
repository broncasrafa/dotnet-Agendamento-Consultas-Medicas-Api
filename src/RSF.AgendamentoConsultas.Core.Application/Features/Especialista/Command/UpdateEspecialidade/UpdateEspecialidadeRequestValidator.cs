using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialidade;

public class UpdateEspecialidadeRequestValidator : AbstractValidator<UpdateEspecialidadeRequest>
{
    public UpdateEspecialidadeRequestValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("O ID do registro deve ser maior que 0");

        RuleFor(c => c.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.EspecialidadeId)
            .GreaterThan(0).WithMessage("O ID da Especialidade deve ser maior que 0");

        RuleFor(c => c.TipoEspecialidade)
            .NotEmpty().WithMessage("O Tipo de Especialidade é obrigatório, não pode ser nulo ou vazio")
            .Must(tipo =>
                    tipo.Equals("Principal", StringComparison.InvariantCultureIgnoreCase) ||
                    tipo.Equals("SubEspecialidade", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("O Tipo de Especialidade deve ser 'Principal' ou 'SubEspecialidade'");
    }
}
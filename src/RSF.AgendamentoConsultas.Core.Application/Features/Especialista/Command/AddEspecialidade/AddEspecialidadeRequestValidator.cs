using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddEspecialidade;

public class AddEspecialidadeRequestValidator : AbstractValidator<AddEspecialidadeRequest>
{
    public AddEspecialidadeRequestValidator()
    {
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
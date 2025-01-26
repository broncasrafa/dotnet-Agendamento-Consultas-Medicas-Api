using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteEspecialidade;

public class DeleteEspecialidadeRequestValidator : AbstractValidator<DeleteEspecialidadeRequest>
{
    public DeleteEspecialidadeRequestValidator()
    {
        RuleFor(c => c.EspecialistaId)
        .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.EspecialidadeId)
            .GreaterThan(0).WithMessage("O ID da Especialidade deve ser maior que 0");
    }
}
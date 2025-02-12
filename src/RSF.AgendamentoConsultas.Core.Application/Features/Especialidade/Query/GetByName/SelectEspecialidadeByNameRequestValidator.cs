using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetByName;

public class SelectEspecialidadeByNameRequestValidator : AbstractValidator<SelectEspecialidadeByNameRequest>
{
    public SelectEspecialidadeByNameRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O nome da Especialidade é obrigatória")
            .NotNull().WithMessage("O nome da Especialidade não deve ser nulo")
            .MinimumLength(3).WithMessage("O nome da Especialidade deve ter no mínimo 3 caracteres");
    }
}
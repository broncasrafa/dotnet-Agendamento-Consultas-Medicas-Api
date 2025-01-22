using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;

public class SelectPacienteDependenteByIdRequestValidator : AbstractValidator<SelectPacienteDependenteByIdRequest>
{
    public SelectPacienteDependenteByIdRequestValidator()
    {
        RuleFor(x => x.DependenteId)
            .GreaterThan(0)
            .WithMessage("O ID do Dependente deve ser maior que 0");
    }
}
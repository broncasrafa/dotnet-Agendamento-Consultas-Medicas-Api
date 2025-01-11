using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public class SelectPacienteDependentePlanosMedicosRequestValidator : AbstractValidator<SelectPacienteDependentePlanosMedicosRequest>
{
    public SelectPacienteDependentePlanosMedicosRequestValidator()
    {
        RuleFor(x => x.DependenteId)
            .GreaterThan(0)
            .WithMessage("O ID do Dependente deve ser maior que 0");
    }
}
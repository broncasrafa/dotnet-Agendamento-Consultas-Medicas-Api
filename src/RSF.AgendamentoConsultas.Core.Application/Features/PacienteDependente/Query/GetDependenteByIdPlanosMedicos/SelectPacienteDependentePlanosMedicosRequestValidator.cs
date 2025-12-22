using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public class SelectPacienteDependentePlanosMedicosRequestValidator : AbstractValidator<SelectPacienteDependentePlanosMedicosRequest>
{
    public SelectPacienteDependentePlanosMedicosRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
    }
}
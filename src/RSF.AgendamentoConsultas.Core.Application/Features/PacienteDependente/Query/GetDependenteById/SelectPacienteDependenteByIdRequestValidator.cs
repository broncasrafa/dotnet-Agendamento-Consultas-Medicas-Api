using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;

public class SelectPacienteDependenteByIdRequestValidator : AbstractValidator<SelectPacienteDependenteByIdRequest>
{
    public SelectPacienteDependenteByIdRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
    }
}
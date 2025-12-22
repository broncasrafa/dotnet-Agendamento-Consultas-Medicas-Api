using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.SearchByName;

public class SelectCidadesByNameRequestValidator : AbstractValidator<SelectCidadesByNameRequest>
{
    public SelectCidadesByNameRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotNullOrEmptyValidators("Nome da cidade", minLength: 3);
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.SearchByName;

public class SelectCidadesByNameRequestValidator : AbstractValidator<SelectCidadesByNameRequest>
{
    public SelectCidadesByNameRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Nome da cidade é obrigatório para efetuar a busca")
            .MinimumLength(3).WithMessage("Nome da cidade deve ter no mínimo 3 caracteres para efetuar a busca");
    }
}
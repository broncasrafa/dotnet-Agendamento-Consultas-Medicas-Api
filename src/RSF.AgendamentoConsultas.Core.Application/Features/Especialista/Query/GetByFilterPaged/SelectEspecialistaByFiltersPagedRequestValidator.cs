using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByFilterPaged;

public class SelectEspecialistaByFiltersPagedRequestValidator : AbstractValidator<SelectEspecialistaByFiltersPagedRequest>
{
    public SelectEspecialistaByFiltersPagedRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
        RuleFor(x => x.Cidade)
            .NotNullOrEmptyValidators("nome da Cidade", minLength: 2)
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome da Cidade deve conter apenas letras e espaços.");
    }
}
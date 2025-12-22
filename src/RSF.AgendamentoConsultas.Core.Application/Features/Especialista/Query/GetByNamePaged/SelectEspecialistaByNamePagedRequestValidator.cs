using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByNamePaged;

public class SelectEspecialistaByNamePagedRequestValidator : AbstractValidator<SelectEspecialistaByNamePagedRequest>
{
    public SelectEspecialistaByNamePagedRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
        RuleFor(x => x.Name)
            .NotNullOrEmptyValidators("nome", minLength: 2)
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras e espaços.");
    }
}
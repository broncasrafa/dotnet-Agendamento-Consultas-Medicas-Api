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
            .NotEmpty().WithMessage("O nome da Cidade não pode ser nulo ou vazio.")
            .MinimumLength(2).WithMessage("O nome da Cidade deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome da Cidade deve conter apenas letras e espaços.");
    }
}
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
            .NotEmpty().WithMessage("O nome não pode ser nulo ou vazio.")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras e espaços.");
    }
}
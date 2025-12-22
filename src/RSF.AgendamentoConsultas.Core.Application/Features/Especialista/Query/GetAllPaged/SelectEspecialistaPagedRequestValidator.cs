using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetAllPaged;

public class SelectEspecialistaPagedRequestValidator : AbstractValidator<SelectEspecialistaPagedRequest>
{
    public SelectEspecialistaPagedRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
    }
}
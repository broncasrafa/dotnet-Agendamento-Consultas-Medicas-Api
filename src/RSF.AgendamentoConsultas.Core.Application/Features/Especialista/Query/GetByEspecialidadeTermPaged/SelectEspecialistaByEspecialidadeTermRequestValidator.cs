using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByEspecialidadeTermPaged;

public class SelectEspecialistaByEspecialidadeTermRequestValidator : AbstractValidator<SelectEspecialistaByEspecialidadeTermRequest>
{
    public SelectEspecialistaByEspecialidadeTermRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
        RuleFor(x => x.EspecialidadeTerm).NotNullOrEmptyValidators(field: "Termo da especialidade", minLength: 2);
    }
}

using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetAllPaged;

public class SelectPerguntaPagedRequestValidator : AbstractValidator<SelectPerguntaPagedRequest>
{
    public SelectPerguntaPagedRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
    }
}
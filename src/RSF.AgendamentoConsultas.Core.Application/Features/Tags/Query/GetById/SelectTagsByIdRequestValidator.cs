using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetById;

public class SelectTagsByIdRequestValidator : AbstractValidator<SelectTagsByIdRequest>
{
    public SelectTagsByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("tag");
    }
}
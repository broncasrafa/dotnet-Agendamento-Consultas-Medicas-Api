using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.GetById;

public class SelectTagsByIdRequestValidator : AbstractValidator<SelectTagsByIdRequest>
{
    public SelectTagsByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da tag deve ser maior que 0");
    }
}
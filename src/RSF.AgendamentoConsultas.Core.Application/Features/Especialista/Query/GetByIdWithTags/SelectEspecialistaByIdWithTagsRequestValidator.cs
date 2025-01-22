using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;

public class SelectEspecialistaByIdWithTagsRequestValidator : AbstractValidator<SelectEspecialistaByIdWithTagsRequest>
{
    public SelectEspecialistaByIdWithTagsRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithTags;

public class SelectEspecialistaByIdWithTagsRequestValidator : AbstractValidator<SelectEspecialistaByIdWithTagsRequest>
{
    public SelectEspecialistaByIdWithTagsRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
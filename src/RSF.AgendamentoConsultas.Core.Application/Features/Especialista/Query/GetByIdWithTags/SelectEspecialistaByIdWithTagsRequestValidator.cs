using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;

public class SelectEspecialistaByIdWithTagsRequestValidator : AbstractValidator<SelectEspecialistaByIdWithTagsRequest>
{
    public SelectEspecialistaByIdWithTagsRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
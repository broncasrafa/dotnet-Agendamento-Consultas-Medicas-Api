using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.GetAll;

public class SelectTagsRequestValidator : AbstractValidator<SelectTagsRequest>
{
    public SelectTagsRequestValidator()
    {
    }
}
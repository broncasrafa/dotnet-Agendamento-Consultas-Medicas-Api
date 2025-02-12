using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetAll;

public class SelectTagsRequestValidator : AbstractValidator<SelectTagsRequest>
{
    public SelectTagsRequestValidator()
    {
    }
}
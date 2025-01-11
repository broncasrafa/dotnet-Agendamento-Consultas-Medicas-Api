using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Tags.GetAll;

public class SelectTagsRequestValidator : AbstractValidator<SelectTagsRequest>
{
    public SelectTagsRequestValidator()
    {
    }
}
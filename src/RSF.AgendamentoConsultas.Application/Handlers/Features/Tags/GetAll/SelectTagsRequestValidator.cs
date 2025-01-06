using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetAll;

public class SelectTagsRequestValidator : AbstractValidator<SelectTagsRequest>
{
    public SelectTagsRequestValidator()
    {
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Query.GetAvaliacaoById;

public class SelectAvaliacaoByIdRequestValidator : AbstractValidator<SelectAvaliacaoByIdRequest>
{
    public SelectAvaliacaoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("avaliação");
    }
}
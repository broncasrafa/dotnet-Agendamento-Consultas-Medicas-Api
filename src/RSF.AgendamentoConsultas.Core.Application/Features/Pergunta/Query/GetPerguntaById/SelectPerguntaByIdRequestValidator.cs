using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestValidator : AbstractValidator<SelectPerguntaByIdRequest>
{
    public SelectPerguntaByIdRequestValidator()
    {
        RuleFor(x => x.PerguntaId).IdValidators("Pergunta");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;

public class SelectPerguntaByIdRespostasRequestValidator : AbstractValidator<SelectPerguntaByIdRespostasRequest>
{
    public SelectPerguntaByIdRespostasRequestValidator()
    {
        RuleFor(x => x.PerguntaId).IdValidators("Pergunta");
    }
}
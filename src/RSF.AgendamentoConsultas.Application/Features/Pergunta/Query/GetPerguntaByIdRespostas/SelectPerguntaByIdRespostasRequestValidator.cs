using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;

public class SelectPerguntaByIdRespostasRequestValidator : AbstractValidator<SelectPerguntaByIdRespostasRequest>
{
    public SelectPerguntaByIdRespostasRequestValidator()
    {
        RuleFor(x => x.PerguntaId)
            .GreaterThan(0)
            .WithMessage("O ID da Pergunta deve ser maior que 0");
    }
}
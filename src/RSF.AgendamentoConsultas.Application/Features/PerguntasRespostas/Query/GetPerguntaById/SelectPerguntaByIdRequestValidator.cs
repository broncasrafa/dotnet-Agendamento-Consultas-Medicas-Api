﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestValidator : AbstractValidator<SelectPerguntaByIdRequest>
{
    public SelectPerguntaByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da pergunta deve ser maior que 0");
    }
}
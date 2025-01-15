﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestValidator : AbstractValidator<SelectPerguntaByIdRequest>
{
    public SelectPerguntaByIdRequestValidator()
    {
        RuleFor(x => x.PerguntaId)
            .GreaterThan(0)
            .WithMessage("O ID da Pergunta deve ser maior que 0");

        RuleFor(x => x.EspecialidadeId)
            .GreaterThan(0)
            .WithMessage("O ID da Especialidade deve ser maior que 0");
    }
}
﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Query.GetAvaliacaoById;

public class SelectAvaliacaoByIdRequestValidator : AbstractValidator<SelectAvaliacaoByIdRequest>
{
    public SelectAvaliacaoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da avaliação deve ser maior que 0");
    }
}
﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithLocaisAtendimento;

public class SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator : AbstractValidator<SelectEspecialistaByIdWithLocaisAtendimentoRequest>
{
    public SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
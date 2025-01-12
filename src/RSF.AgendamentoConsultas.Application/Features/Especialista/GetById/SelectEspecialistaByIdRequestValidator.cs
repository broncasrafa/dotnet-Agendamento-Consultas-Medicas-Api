﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetById;

public class SelectEspecialistaByIdRequestValidator : AbstractValidator<SelectEspecialistaByIdRequest>
{
    public SelectEspecialistaByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
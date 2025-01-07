﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithConveniosMedicos;

public class SelectEspecialistaByIdWithConveniosMedicosRequestValidator : AbstractValidator<SelectEspecialistaByIdWithConveniosMedicosRequest>
{
    public SelectEspecialistaByIdWithConveniosMedicosRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
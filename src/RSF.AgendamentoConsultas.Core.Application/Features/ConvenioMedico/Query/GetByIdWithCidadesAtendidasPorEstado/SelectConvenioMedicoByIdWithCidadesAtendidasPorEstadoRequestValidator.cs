﻿using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetByIdWithCidadesAtendidasPorEstado;

public class SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestValidator : AbstractValidator<SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest>
{
    public SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do convênio médico deve ser maior que 0");

        RuleFor(x => x.EstadoId)
            .GreaterThan(0)
            .WithMessage("O ID do estado deve ser maior que 0");
    }
}
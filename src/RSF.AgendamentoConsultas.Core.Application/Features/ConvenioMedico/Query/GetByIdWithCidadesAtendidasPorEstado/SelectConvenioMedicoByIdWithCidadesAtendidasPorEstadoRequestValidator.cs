using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetByIdWithCidadesAtendidasPorEstado;

public class SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestValidator : AbstractValidator<SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest>
{
    public SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("convênio médico");
        RuleFor(x => x.EstadoId).IdValidators("estado");
    }
}
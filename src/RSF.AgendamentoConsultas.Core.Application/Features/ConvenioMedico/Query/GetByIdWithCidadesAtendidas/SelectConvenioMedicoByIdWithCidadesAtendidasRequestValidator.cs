using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetByIdWithCidadesAtendidas;

public class SelectConvenioMedicoByIdWithCidadesAtendidasRequestValidator : AbstractValidator<SelectConvenioMedicoByIdWithCidadesAtendidasRequest>
{
    public SelectConvenioMedicoByIdWithCidadesAtendidasRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("convênio médico");
    }
}
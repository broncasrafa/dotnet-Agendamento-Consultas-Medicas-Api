using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateConvenioMedico;

public class UpdateConvenioMedicoRequestValidator : AbstractValidator<UpdateConvenioMedicoRequest>
{
    public UpdateConvenioMedicoRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("registro");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Médico");
    }
}
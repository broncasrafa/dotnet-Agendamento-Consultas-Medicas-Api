using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddConvenioMedico;

public class AddConvenioMedicoRequestValidator : AbstractValidator<AddConvenioMedicoRequest>
{
    public AddConvenioMedicoRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Médico");
    }
}
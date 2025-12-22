using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteConvenioMedico;

public class DeleteConvenioMedicoRequestValidator : AbstractValidator<DeleteConvenioMedicoRequest>
{
    public DeleteConvenioMedicoRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Médico");
    }
}
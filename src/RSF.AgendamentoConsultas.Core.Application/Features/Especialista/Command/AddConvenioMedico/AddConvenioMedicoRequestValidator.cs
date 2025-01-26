using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddConvenioMedico;

public class AddConvenioMedicoRequestValidator : AbstractValidator<AddConvenioMedicoRequest>
{
    public AddConvenioMedicoRequestValidator()
    {
        RuleFor(c => c.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.ConvenioMedicoId)
            .GreaterThan(0).WithMessage("O ID do Convênio Médico deve ser maior que 0");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateConvenioMedico;

public class UpdateConvenioMedicoRequestValidator : AbstractValidator<UpdateConvenioMedicoRequest>
{
    public UpdateConvenioMedicoRequestValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("O ID do registro deve ser maior que 0");

        RuleFor(c => c.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.ConvenioMedicoId)
            .GreaterThan(0).WithMessage("O ID do Convênio Médico deve ser maior que 0");
    }
}
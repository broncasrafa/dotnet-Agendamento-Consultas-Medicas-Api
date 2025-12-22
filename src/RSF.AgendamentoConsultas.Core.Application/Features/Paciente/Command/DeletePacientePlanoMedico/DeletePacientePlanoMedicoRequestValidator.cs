using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacientePlanoMedico;

public class DeletePacientePlanoMedicoRequestValidator : AbstractValidator<DeletePacientePlanoMedicoRequest>
{
    public DeletePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.PlanoMedicoId).IdValidators("Plano Medico");
    }
}
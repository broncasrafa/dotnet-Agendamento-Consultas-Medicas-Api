using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePaciente;

public class DeletePacienteRequestValidator : AbstractValidator<DeletePacienteRequest>
{
    public DeletePacienteRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
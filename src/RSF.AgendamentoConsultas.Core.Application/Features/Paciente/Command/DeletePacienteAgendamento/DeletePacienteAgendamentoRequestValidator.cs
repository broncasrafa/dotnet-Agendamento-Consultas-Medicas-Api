using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacienteAgendamento;

public class DeletePacienteAgendamentoRequestValidator : AbstractValidator<DeletePacienteAgendamentoRequest>
{
    public DeletePacienteAgendamentoRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.AgendamentoId).IdValidators("Agendamento");
        RuleFor(x => x.NotaCancelamento)
            .NotNullOrEmptyValidators("Nota do Cancelamento", minLength: 5);
    }
}
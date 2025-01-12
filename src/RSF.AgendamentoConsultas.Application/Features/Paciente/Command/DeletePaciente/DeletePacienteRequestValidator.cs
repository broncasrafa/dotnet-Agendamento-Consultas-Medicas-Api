using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePaciente;

public class DeletePacienteRequestValidator : AbstractValidator<DeletePacienteRequest>
{
    public DeletePacienteRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
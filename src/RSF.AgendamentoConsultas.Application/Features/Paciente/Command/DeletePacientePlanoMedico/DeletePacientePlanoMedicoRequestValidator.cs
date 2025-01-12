using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePacientePlanoMedico;

public class DeletePacientePlanoMedicoRequestValidator : AbstractValidator<DeletePacientePlanoMedicoRequest>
{
    public DeletePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.PlanoMedicoId)
            .GreaterThan(0)
            .WithMessage("O ID do Plano Medico deve ser maior que 0");
    }
}
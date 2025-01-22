using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacienteAgendamento;

public class DeletePacienteAgendamentoRequestValidator : AbstractValidator<DeletePacienteAgendamentoRequest>
{
    public DeletePacienteAgendamentoRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.AgendamentoId)
            .GreaterThan(0)
            .WithMessage("O ID do Agendamento deve ser maior que 0");

        RuleFor(x => x.NotaCancelamento)
            .NotEmpty().WithMessage("A Nota do Cancelamento é obrigatória, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("A Nota do Cancelamento deve ter no minimo 5 caracteres");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public class SelectPacienteAgendamentosRequestValidator : AbstractValidator<SelectPacienteAgendamentosRequest>
{
    public SelectPacienteAgendamentosRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
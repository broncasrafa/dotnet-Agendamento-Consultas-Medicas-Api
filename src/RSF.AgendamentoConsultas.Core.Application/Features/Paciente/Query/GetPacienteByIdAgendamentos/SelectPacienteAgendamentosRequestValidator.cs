using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public class SelectPacienteAgendamentosRequestValidator : AbstractValidator<SelectPacienteAgendamentosRequest>
{
    public SelectPacienteAgendamentosRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
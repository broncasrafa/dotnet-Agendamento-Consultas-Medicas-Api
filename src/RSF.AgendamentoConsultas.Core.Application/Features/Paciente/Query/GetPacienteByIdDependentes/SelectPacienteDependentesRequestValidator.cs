using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestValidator : AbstractValidator<SelectPacienteDependentesRequest>
{
    public SelectPacienteDependentesRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
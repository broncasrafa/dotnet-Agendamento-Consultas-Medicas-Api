using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public class SelectPacientePlanosMedicosRequestValidator : AbstractValidator<SelectPacientePlanosMedicosRequest>
{
    public SelectPacientePlanosMedicosRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
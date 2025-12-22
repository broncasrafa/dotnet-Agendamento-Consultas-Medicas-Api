using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestValidator : AbstractValidator<SelectPacienteByIdRequest>
{
    public SelectPacienteByIdRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
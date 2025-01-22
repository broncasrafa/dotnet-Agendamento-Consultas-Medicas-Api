using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestValidator : AbstractValidator<SelectPacienteByIdRequest>
{
    public SelectPacienteByIdRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
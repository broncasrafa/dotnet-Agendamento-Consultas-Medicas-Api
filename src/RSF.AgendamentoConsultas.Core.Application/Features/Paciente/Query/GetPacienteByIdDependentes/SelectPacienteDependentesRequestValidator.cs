using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestValidator : AbstractValidator<SelectPacienteDependentesRequest>
{
    public SelectPacienteDependentesRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
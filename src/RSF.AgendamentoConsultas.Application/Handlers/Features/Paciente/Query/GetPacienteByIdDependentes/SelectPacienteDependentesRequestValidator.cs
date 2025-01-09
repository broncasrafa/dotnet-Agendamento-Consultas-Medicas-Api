using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestValidator : AbstractValidator<SelectPacienteDependentesRequest>
{
    public SelectPacienteDependentesRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
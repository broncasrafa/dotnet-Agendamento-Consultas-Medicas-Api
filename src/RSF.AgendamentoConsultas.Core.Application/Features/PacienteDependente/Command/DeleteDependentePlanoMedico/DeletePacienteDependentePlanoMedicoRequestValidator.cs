using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;

public class DeletePacienteDependentePlanoMedicoRequestValidator : AbstractValidator<DeletePacienteDependentePlanoMedicoRequest>
{
    public DeletePacienteDependentePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PlanoMedicoId)
            .GreaterThan(0).WithMessage("O ID do Plano Médico do Dependente deve ser maior que 0");

        RuleFor(x => x.DependenteId)
            .GreaterThan(0).WithMessage("O ID do Dependente deve ser maior que 0");

        RuleFor(x => x.PacientePrincipalId)
            .GreaterThan(0).WithMessage("O ID do Paciente Principal deve ser maior que 0");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;

public class DeletePacienteDependentePlanoMedicoRequestValidator : AbstractValidator<DeletePacienteDependentePlanoMedicoRequest>
{
    public DeletePacienteDependentePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
        RuleFor(x => x.PacientePrincipalId).IdValidators("Paciente Principal");
        RuleFor(x => x.PlanoMedicoId).IdValidators("Plano Médico do Dependente");
    }
}
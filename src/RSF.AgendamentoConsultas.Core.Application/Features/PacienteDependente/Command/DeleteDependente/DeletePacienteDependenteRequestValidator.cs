using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependente;

public class DeletePacienteDependenteRequestValidator : AbstractValidator<DeletePacienteDependenteRequest>
{
    public DeletePacienteDependenteRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
        RuleFor(x => x.PacientePrincipalId).IdValidators("Paciente Principal");
    }
}
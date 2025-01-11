using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependente;

public class DeletePacienteDependenteRequestValidator : AbstractValidator<DeletePacienteDependenteRequest>
{
    public DeletePacienteDependenteRequestValidator()
    {
        RuleFor(x => x.DependenteId)
        .GreaterThan(0).WithMessage("O ID do Dependente deve ser maior que 0");

        RuleFor(x => x.PacientePrincipalId)
            .GreaterThan(0).WithMessage("O ID do Paciente Principal deve ser maior que 0");
    }
}
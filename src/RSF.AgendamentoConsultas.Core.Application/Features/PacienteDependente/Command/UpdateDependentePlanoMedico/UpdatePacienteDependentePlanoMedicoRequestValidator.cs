using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;

public class UpdatePacienteDependentePlanoMedicoRequestValidator : AbstractValidator<UpdatePacienteDependentePlanoMedicoRequest>
{
    public UpdatePacienteDependentePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
        RuleFor(x => x.PacientePrincipalId).IdValidators("Paciente Principal");
        RuleFor(x => x.PlanoMedicoId).IdValidators("Plano Médico do Dependente");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Medico");
        RuleFor(c => c.NomePlano)
            .NotNullOrEmptyValidators("Nome do Plano", minLength: 3);

        RuleFor(c => c.NumeroCarteirinha)
            .NotNullOrEmptyValidators("Número da carteirinha", minLength: 5)
            .Matches(@"^\d+$").WithMessage("O Número da carteirinha deve conter apenas números.");
    }
}
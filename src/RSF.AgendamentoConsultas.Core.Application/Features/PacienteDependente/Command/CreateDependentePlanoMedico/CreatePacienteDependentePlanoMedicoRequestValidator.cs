using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public class CreatePacienteDependentePlanoMedicoRequestValidator : AbstractValidator<CreatePacienteDependentePlanoMedicoRequest>
{
    public CreatePacienteDependentePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Paciente Dependente");
        RuleFor(x => x.PacientePrincipalId).IdValidators("Paciente Principal");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Medico");
        RuleFor(c => c.NomePlano).NotNullOrEmptyValidators("Nome do Plano", minLength: 3);
        RuleFor(c => c.NumCartao)
            .NotNullOrEmptyValidators("Número do cartão", minLength: 5)
            .Matches(@"^\d+$").WithMessage("O Número do cartão deve conter apenas números.");
    }
}
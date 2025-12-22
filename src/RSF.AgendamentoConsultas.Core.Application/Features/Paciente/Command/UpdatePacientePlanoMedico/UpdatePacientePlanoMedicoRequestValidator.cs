using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePacientePlanoMedico;

public class UpdatePacientePlanoMedicoRequestValidator : AbstractValidator<UpdatePacientePlanoMedicoRequest>
{
    public UpdatePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PlanoMedicoId).IdValidators("Plano Médico");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(c => c.NomePlano).NotNullOrEmptyValidators("Nome do Plano", minLength: 3);
        RuleFor(c => c.NumeroCarteirinha)
            .NotNullOrEmptyValidators("Número da carteirinha", minLength: 5)
            .Matches(@"^\d+$").WithMessage("O Número da carteirinha deve conter apenas números.");
    }
}
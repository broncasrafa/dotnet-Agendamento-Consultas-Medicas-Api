using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.CreatePacientePlanoMedico;

public class CreatePacientePlanoMedicoRequestValidator : AbstractValidator<CreatePacientePlanoMedicoRequest>
{
    public CreatePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.ConvenioMedicoId).IdValidators("Convênio Medico");
        RuleFor(c => c.NomePlano)
            .NotNullOrEmptyValidators("Nome do Plano", minLength: 3);

        RuleFor(c => c.NumCartao)
            .NotNullOrEmptyValidators("Número do cartão", minLength: 5)
            .Matches(@"^\d+$").WithMessage("O Número do cartão deve conter apenas números.");
    }
}
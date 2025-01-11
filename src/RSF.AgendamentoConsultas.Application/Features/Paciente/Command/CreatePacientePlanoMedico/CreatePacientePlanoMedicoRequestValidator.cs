using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.CreatePacientePlanoMedico;

public class CreatePacientePlanoMedicoRequestValidator : AbstractValidator<CreatePacientePlanoMedicoRequest>
{
    public CreatePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.ConvenioMedicoId)
            .GreaterThan(0)
            .WithMessage("O ID do Convênio Medico deve ser maior que 0");

        RuleFor(c => c.NomePlano)
            .NotEmpty().WithMessage("O Nome do Plano é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(3).WithMessage("O Nome do Plano deve ter pelo menos 3 caracteres");

        RuleFor(c => c.NumCartao)
            .NotEmpty().WithMessage("O Número do cartão é obrigatório, não deve ser nulo ou vazio")
            .Matches(@"^\d+$").WithMessage("O Número do cartão deve conter apenas números.")
            .MinimumLength(5).WithMessage("O Número do cartão deve ter pelo menos 5 caracteres");
    }
}
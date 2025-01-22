using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePacientePlanoMedico;

public class UpdatePacientePlanoMedicoRequestValidator : AbstractValidator<UpdatePacientePlanoMedicoRequest>
{
    public UpdatePacientePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PlanoMedicoId)
            .GreaterThan(0).WithMessage("O ID do Plano Médico deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(c => c.NomePlano)
            .NotEmpty().WithMessage("O Nome do Plano é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(3).WithMessage("O Nome do Plano deve ter pelo menos 3 caracteres");

        RuleFor(c => c.NumeroCarteirinha)
            .NotEmpty().WithMessage("O Número da carteirinha é obrigatório, não deve ser nulo ou vazio")
            .Matches(@"^\d+$").WithMessage("O Número da carteirinha deve conter apenas números.")
            .MinimumLength(5).WithMessage("O Número da carteirinha deve ter pelo menos 5 caracteres");
    }
}
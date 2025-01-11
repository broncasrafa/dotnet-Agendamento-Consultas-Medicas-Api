using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;

public class UpdatePacienteDependentePlanoMedicoRequestValidator : AbstractValidator<UpdatePacienteDependentePlanoMedicoRequest>
{
    public UpdatePacienteDependentePlanoMedicoRequestValidator()
    {
        RuleFor(x => x.PlanoMedicoId)
            .GreaterThan(0).WithMessage("O ID do Plano Médico do Dependente deve ser maior que 0");

        RuleFor(x => x.DependenteId)
            .GreaterThan(0).WithMessage("O ID do Dependente deve ser maior que 0");

        RuleFor(x => x.PacientePrincipalId)
            .GreaterThan(0).WithMessage("O ID do Paciente Principal deve ser maior que 0");

        RuleFor(x => x.ConvenioMedicoId)
            .GreaterThan(0).WithMessage("O ID do Convênio Medico deve ser maior que 0");

        RuleFor(c => c.NomePlano)
            .NotEmpty().WithMessage("O Nome do Plano é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(3).WithMessage("O Nome do Plano deve ter pelo menos 3 caracteres");

        RuleFor(c => c.NumeroCarteirinha)
            .NotEmpty().WithMessage("O Número da carteirinha é obrigatório, não deve ser nulo ou vazio")
            .Matches(@"^\d+$").WithMessage("O Número da carteirinha deve conter apenas números.")
            .MinimumLength(5).WithMessage("O Número da carteirinha deve ter pelo menos 5 caracteres");
    }
}
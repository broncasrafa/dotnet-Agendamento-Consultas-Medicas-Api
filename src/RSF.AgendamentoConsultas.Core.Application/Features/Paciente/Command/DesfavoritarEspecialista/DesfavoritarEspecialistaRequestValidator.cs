using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DesfavoritarEspecialista;

public class DesfavoritarEspecialistaRequestValidator : AbstractValidator<DesfavoritarEspecialistaRequest>
{
    public DesfavoritarEspecialistaRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");
        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
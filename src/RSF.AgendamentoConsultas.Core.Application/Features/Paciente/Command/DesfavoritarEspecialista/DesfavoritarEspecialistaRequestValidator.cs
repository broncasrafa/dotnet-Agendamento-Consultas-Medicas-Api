using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DesfavoritarEspecialista;

public class DesfavoritarEspecialistaRequestValidator : AbstractValidator<DesfavoritarEspecialistaRequest>
{
    public DesfavoritarEspecialistaRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
    }
}
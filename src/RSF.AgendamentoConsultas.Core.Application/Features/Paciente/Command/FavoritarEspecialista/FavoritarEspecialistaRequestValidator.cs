using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.FavoritarEspecialista;

public class FavoritarEspecialistaRequestValidator : AbstractValidator<FavoritarEspecialistaRequest>
{
    public FavoritarEspecialistaRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Dislike;

public class CreateDislikeRespostaRequestValidator : AbstractValidator<CreateDislikeRespostaRequest>
{
    public CreateDislikeRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId).IdValidators("Resposta");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
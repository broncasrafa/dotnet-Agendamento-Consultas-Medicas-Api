using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Like;

public class CreateLikeRespostaRequestValidator :  AbstractValidator<CreateLikeRespostaRequest>
{
    public CreateLikeRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId).IdValidators("Resposta");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
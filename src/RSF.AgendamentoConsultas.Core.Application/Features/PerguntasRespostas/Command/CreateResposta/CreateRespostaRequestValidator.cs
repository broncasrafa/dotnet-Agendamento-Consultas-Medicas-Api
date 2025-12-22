using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.CreateResposta;

public class CreateRespostaRequestValidator : AbstractValidator<CreateRespostaRequest>
{
    public CreateRespostaRequestValidator()
    {
        RuleFor(x => x.PerguntaId).IdValidators("Pergunta");
        RuleFor(c => c.Resposta)
            .NotNullOrEmptyValidators("texto da Resposta", minLength: 5);
    }
}
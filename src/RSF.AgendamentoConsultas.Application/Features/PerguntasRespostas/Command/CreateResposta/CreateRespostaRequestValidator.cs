using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Command.CreateResposta;

public class CreateRespostaRequestValidator : AbstractValidator<CreateRespostaRequest>
{
    public CreateRespostaRequestValidator()
    {
        RuleFor(x => x.PerguntaId)
            .GreaterThan(0)
            .WithMessage("O ID da Pergunta deve ser maior que 0");

        RuleFor(c => c.Resposta)
            .NotEmpty().WithMessage("O texto da Resposta é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O texto da Resposta deve ter pelo menos 5 caracteres");
    }
}
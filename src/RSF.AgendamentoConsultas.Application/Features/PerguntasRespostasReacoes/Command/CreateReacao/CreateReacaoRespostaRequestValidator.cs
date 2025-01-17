using FluentValidation;

using RSF.AgendamentoConsultas.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;

public class CreateReacaoRespostaRequestValidator : AbstractValidator<CreateReacaoRespostaRequest>
{
    public CreateReacaoRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId)
            .GreaterThan(0)
            .WithMessage("O ID da Resposta deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID da Paciente deve ser maior que 0");

        RuleFor(x => x.Reacao)
            .NotEmpty().WithMessage("A reação é obrigatória, não deve ser nulo ou vazia")
            .Must(reacao => reacao.Equals(ETipoReacaoResposta.Like.ToString(), StringComparison.InvariantCultureIgnoreCase) || 
                            reacao.Equals(ETipoReacaoResposta.Dislike.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .WithMessage("A reação deve ser 'Like' ou 'Dislike'");
    }
}
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;

public class UpdateReacaoRespostaRequestValidator : AbstractValidator<UpdateReacaoRespostaRequest>
{
    public UpdateReacaoRespostaRequestValidator()
    {
        RuleFor(x => x.ReacaoId)
            .GreaterThan(0)
            .WithMessage("O ID da Reação deve ser maior que 0");

        RuleFor(x => x.RespostaId)
            .GreaterThan(0)
            .WithMessage("O ID da Resposta deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID da Paciente deve ser maior que 0");

        RuleFor(x => x.Reacao)
            .NotEmpty().When(x => x.Reacao != null)
            .Must(reacao => reacao.Equals(ETipoReacaoResposta.Like.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                            reacao.Equals(ETipoReacaoResposta.Dislike.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .When(x => x.Reacao != null).WithMessage("A reação deve ser 'Like' ou 'Dislike'");
    }
}
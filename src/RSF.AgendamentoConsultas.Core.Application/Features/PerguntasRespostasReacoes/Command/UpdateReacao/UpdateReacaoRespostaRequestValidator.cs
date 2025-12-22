using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;

public class UpdateReacaoRespostaRequestValidator : AbstractValidator<UpdateReacaoRespostaRequest>
{
    public UpdateReacaoRespostaRequestValidator()
    {
        RuleFor(x => x.ReacaoId).IdValidators("Reação");
        RuleFor(x => x.RespostaId).IdValidators("Resposta");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");

        RuleFor(x => x.Reacao)
            .NotEmpty().When(x => x.Reacao != null)
            .Must(reacao => reacao.Equals(ETipoReacaoResposta.Like.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
                            reacao.Equals(ETipoReacaoResposta.Dislike.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .When(x => x.Reacao != null).WithMessage("A reação deve ser 'Like' ou 'Dislike'");
    }
}
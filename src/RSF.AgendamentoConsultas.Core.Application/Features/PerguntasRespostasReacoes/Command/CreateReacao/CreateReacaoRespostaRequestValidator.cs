using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;

public class CreateReacaoRespostaRequestValidator : AbstractValidator<CreateReacaoRespostaRequest>
{
    public CreateReacaoRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId).IdValidators("Resposta");
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.Reacao)
            .NotEmpty().WithMessage("A reação é obrigatória, não deve ser nulo ou vazia")
            .Must(reacao => reacao.Equals(ETipoReacaoResposta.Like.ToString(), StringComparison.InvariantCultureIgnoreCase) || 
                            reacao.Equals(ETipoReacaoResposta.Dislike.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .WithMessage("A reação deve ser 'Like' ou 'Dislike'");
    }
}
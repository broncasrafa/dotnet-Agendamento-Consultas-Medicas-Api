using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByIdReacoes;

public class SelectRespostaByIdReacoesRequestValidator : AbstractValidator<SelectRespostaByIdReacoesRequest>
{
    public SelectRespostaByIdReacoesRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da resposta deve ser maior que 0");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasById;

public class SelectRespostaByIdRequestValidator : AbstractValidator<SelectRespostaByIdRequest>
{
    public SelectRespostaByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da resposta deve ser maior que 0");
    }
}
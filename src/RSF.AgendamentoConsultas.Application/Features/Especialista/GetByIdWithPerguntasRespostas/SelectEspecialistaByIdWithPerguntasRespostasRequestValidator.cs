using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithPerguntasRespostas;

public class SelectEspecialistaByIdWithPerguntasRespostasRequestValidator : AbstractValidator<SelectEspecialistaByIdWithPerguntasRespostasRequest>
{
    public SelectEspecialistaByIdWithPerguntasRespostasRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
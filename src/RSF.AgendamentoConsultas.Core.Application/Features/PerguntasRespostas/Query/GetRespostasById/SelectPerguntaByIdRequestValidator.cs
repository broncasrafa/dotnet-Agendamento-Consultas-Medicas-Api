using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Query.GetRespostasById;

public class SelectRespostaByIdRequestValidator : AbstractValidator<SelectRespostaByIdRequest>
{
    public SelectRespostaByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Resposta");
    }
}
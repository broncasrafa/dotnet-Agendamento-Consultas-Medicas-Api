using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdAndIdEspecialidade;

public class SelectPerguntaByIdAndIdEspecialidadeRequestValidator : AbstractValidator<SelectPerguntaByIdAndIdEspecialidadeRequest>
{
    public SelectPerguntaByIdAndIdEspecialidadeRequestValidator()
    {
        RuleFor(x => x.PerguntaId).IdValidators("Pergunta");
        RuleFor(x => x.EspecialidadeId).IdValidators("Especialidade");
    }
}
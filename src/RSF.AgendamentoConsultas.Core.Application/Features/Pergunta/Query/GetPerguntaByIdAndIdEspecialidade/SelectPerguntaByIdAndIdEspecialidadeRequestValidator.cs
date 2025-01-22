using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdAndIdEspecialidade;

public class SelectPerguntaByIdAndIdEspecialidadeRequestValidator : AbstractValidator<SelectPerguntaByIdAndIdEspecialidadeRequest>
{
    public SelectPerguntaByIdAndIdEspecialidadeRequestValidator()
    {
        RuleFor(x => x.PerguntaId)
            .GreaterThan(0)
            .WithMessage("O ID da Pergunta deve ser maior que 0");

        RuleFor(x => x.EspecialidadeId)
            .GreaterThan(0)
            .WithMessage("O ID da Especialidade deve ser maior que 0");
    }
}
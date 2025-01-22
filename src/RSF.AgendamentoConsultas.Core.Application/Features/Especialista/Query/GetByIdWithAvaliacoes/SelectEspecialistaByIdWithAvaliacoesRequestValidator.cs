using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithAvaliacoes;

public class SelectEspecialistaByIdWithAvaliacoesRequestValidator : AbstractValidator<SelectEspecialistaByIdWithAvaliacoesRequest>
{
    public SelectEspecialistaByIdWithAvaliacoesRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithAvaliacoes;

public class SelectEspecialistaByIdWithAvaliacoesRequestValidator : AbstractValidator<SelectEspecialistaByIdWithAvaliacoesRequest>
{
    public SelectEspecialistaByIdWithAvaliacoesRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
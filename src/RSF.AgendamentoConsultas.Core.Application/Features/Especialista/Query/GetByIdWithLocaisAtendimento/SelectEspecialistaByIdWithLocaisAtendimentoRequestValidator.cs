using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithLocaisAtendimento;

public class SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator : AbstractValidator<SelectEspecialistaByIdWithLocaisAtendimentoRequest>
{
    public SelectEspecialistaByIdWithLocaisAtendimentoRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
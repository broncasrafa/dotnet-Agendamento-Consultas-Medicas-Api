using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaRespostas;

public class SelectEspecialistaRespostasRequestValidator : AbstractValidator<SelectEspecialistaRespostasRequest>
{
    public SelectEspecialistaRespostasRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
    }
}
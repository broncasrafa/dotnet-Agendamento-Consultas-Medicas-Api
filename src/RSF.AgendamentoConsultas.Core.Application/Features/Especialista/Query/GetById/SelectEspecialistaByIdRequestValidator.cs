using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetById;

public class SelectEspecialistaByIdRequestValidator : AbstractValidator<SelectEspecialistaByIdRequest>
{
    public SelectEspecialistaByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByUserId;

public class SelectEspecialistaByUserIdRequestValidator : AbstractValidator<SelectEspecialistaByUserIdRequest>
{
    public SelectEspecialistaByUserIdRequestValidator()
    {
        RuleFor(x => x.UserId).IdValidators("Especialista");
    }
}
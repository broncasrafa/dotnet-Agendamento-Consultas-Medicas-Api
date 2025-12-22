using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Query.GetById;

public class SelectRegiaoByIdRequestValidator : AbstractValidator<SelectRegiaoByIdRequest>
{
    public SelectRegiaoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("região");
    }
}
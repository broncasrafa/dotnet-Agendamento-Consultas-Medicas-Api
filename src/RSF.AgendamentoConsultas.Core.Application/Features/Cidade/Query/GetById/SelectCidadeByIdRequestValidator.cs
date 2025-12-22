using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetById;

public class SelectCidadeByIdRequestValidator : AbstractValidator<SelectCidadeByIdRequest>
{
    public SelectCidadeByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("cidade");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetById;

public class SelectCidadeByIdRequestValidator : AbstractValidator<SelectCidadeByIdRequest>
{
    public SelectCidadeByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da cidade deve ser maior que 0");
    }
}
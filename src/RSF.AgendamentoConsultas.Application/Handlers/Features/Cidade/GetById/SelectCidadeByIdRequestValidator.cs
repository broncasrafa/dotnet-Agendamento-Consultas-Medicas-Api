using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

public class SelectCidadeByIdRequestValidator : AbstractValidator<SelectCidadeByIdRequest>
{
    public SelectCidadeByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da cidade deve ser maior que 0");
    }
}
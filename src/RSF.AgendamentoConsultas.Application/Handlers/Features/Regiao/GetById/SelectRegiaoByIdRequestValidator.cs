using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetById;

public class SelectRegiaoByIdRequestValidator : AbstractValidator<SelectRegiaoByIdRequest>
{
    public SelectRegiaoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da região deve ser maior que 0");
    }
}
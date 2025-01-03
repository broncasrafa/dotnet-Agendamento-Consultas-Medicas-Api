using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetByIdWithEstados;

public class SelectRegiaoByIdWithEstadosRequestValidator : AbstractValidator<SelectRegiaoByIdWithEstadosRequest>
{
    public SelectRegiaoByIdWithEstadosRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da região deve ser maior que 0");
    }
}
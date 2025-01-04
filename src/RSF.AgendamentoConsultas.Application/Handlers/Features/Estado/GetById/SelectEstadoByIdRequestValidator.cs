using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetById;

public class SelectEstadoByIdRequestValidator : AbstractValidator<SelectEstadoByIdRequest>
{
    public SelectEstadoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do estado deve ser maior que 0");
    }
}
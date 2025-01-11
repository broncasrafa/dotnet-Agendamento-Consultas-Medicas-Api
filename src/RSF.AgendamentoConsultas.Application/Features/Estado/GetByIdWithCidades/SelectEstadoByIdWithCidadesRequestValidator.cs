using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetByIdWithCidades;

public class SelectEstadoByIdWithCidadesRequestValidator : AbstractValidator<SelectEstadoByIdWithCidadesRequest>
{
    public SelectEstadoByIdWithCidadesRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do estado deve ser maior que 0");
    }
}
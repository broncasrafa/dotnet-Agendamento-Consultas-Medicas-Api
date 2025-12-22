using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetByIdWithCidades;

public class SelectEstadoByIdWithCidadesRequestValidator : AbstractValidator<SelectEstadoByIdWithCidadesRequest>
{
    public SelectEstadoByIdWithCidadesRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("estado");
    }
}
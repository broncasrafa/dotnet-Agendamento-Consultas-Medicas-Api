using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetById;

public class SelectEstadoByIdRequestValidator : AbstractValidator<SelectEstadoByIdRequest>
{
    public SelectEstadoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("estado");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Query.GetById;

public class SelectTipoConsultaByIdRequestValidator : AbstractValidator<SelectTipoConsultaByIdRequest>
{
    public SelectTipoConsultaByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Tipo de Consulta");
    }
}
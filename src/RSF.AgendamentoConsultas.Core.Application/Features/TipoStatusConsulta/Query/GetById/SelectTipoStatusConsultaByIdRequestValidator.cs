using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Query.GetById;

public class SelectTipoStatusConsultaByIdRequestValidator : AbstractValidator<SelectTipoStatusConsultaByIdRequest>
{
    public SelectTipoStatusConsultaByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Tipo de Status da Consulta");
    }
}
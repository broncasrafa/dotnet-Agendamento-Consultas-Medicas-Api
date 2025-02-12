using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Query.GetAll;

public class SelectTipoConsultaRequestValidator : AbstractValidator<SelectTipoConsultaRequest>
{
    public SelectTipoConsultaRequestValidator()
    {
    }
}
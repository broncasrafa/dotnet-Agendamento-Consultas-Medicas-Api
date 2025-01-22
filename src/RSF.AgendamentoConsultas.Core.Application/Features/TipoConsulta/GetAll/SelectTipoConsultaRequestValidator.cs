using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.GetAll;

public class SelectTipoConsultaRequestValidator : AbstractValidator<SelectTipoConsultaRequest>
{
    public SelectTipoConsultaRequestValidator()
    {
    }
}
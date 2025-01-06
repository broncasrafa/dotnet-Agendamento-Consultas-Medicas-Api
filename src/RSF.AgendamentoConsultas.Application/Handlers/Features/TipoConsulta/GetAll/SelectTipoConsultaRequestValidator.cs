using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetAll;

public class SelectTipoConsultaRequestValidator : AbstractValidator<SelectTipoConsultaRequest>
{
    public SelectTipoConsultaRequestValidator()
    {
    }
}
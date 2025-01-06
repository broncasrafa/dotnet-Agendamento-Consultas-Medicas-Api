using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.GetAll;

public class SelectTipoStatusConsultaRequestValidator : AbstractValidator<SelectTipoStatusConsultaRequest>
{
    public SelectTipoStatusConsultaRequestValidator()
    {
    }
}
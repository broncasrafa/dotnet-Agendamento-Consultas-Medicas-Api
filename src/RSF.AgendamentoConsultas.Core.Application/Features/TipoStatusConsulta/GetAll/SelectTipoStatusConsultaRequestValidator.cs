using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetAll;

public class SelectTipoStatusConsultaRequestValidator : AbstractValidator<SelectTipoStatusConsultaRequest>
{
    public SelectTipoStatusConsultaRequestValidator()
    {
    }
}
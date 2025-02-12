using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Query.GetAll;

public class SelectTipoStatusConsultaRequestValidator : AbstractValidator<SelectTipoStatusConsultaRequest>
{
    public SelectTipoStatusConsultaRequestValidator()
    {
    }
}
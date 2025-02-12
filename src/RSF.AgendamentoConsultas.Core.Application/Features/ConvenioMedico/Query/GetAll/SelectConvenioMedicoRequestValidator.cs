using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetAll;

public class SelectConvenioMedicoRequestValidator : AbstractValidator<SelectConvenioMedicoRequest>
{
    public SelectConvenioMedicoRequestValidator()
    {
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetById;

public class SelectConvenioMedicoByIdRequestValidator : AbstractValidator<SelectConvenioMedicoByIdRequest>
{
    public SelectConvenioMedicoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("convênio médico");
    }
}
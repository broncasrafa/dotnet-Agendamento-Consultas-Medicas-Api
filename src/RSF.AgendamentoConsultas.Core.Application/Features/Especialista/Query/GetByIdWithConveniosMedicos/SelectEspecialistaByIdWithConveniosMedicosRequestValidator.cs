using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithConveniosMedicos;

public class SelectEspecialistaByIdWithConveniosMedicosRequestValidator : AbstractValidator<SelectEspecialistaByIdWithConveniosMedicosRequest>
{
    public SelectEspecialistaByIdWithConveniosMedicosRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
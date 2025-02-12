using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetByIdWithCidadesAtendidas;

public class SelectConvenioMedicoByIdWithCidadesAtendidasRequestValidator : AbstractValidator<SelectConvenioMedicoByIdWithCidadesAtendidasRequest>
{
    public SelectConvenioMedicoByIdWithCidadesAtendidasRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do convênio médico deve ser maior que 0");
    }
}
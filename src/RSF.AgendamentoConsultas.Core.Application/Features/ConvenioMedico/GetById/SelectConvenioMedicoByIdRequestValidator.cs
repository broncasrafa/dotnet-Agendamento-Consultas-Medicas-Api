using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetById;

public class SelectConvenioMedicoByIdRequestValidator : AbstractValidator<SelectConvenioMedicoByIdRequest>
{
    public SelectConvenioMedicoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do convênio médico deve ser maior que 0");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithConveniosMedicos;

public class SelectEspecialistaByIdWithConveniosMedicosRequestValidator : AbstractValidator<SelectEspecialistaByIdWithConveniosMedicosRequest>
{
    public SelectEspecialistaByIdWithConveniosMedicosRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
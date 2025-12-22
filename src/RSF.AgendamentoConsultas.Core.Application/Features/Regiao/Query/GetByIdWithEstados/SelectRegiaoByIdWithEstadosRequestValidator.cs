using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Query.GetByIdWithEstados;

public class SelectRegiaoByIdWithEstadosRequestValidator : AbstractValidator<SelectRegiaoByIdWithEstadosRequest>
{
    public SelectRegiaoByIdWithEstadosRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("região");
    }
}
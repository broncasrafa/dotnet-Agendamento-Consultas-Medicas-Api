using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetById;

public class SelectEspecialidadeByIdRequestValidator : AbstractValidator<SelectEspecialidadeByIdRequest>
{
    public SelectEspecialidadeByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("especialidade");
    }
}
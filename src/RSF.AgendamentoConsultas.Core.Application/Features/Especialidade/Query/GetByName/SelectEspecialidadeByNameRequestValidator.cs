using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetByName;

public class SelectEspecialidadeByNameRequestValidator : AbstractValidator<SelectEspecialidadeByNameRequest>
{
    public SelectEspecialidadeByNameRequestValidator()
    {
        RuleFor(c => c.Name).NotNullOrEmptyValidators("nome da Especialidade", minLength: 3);
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetById;

public class SelectEspecialidadeGrupoByIdRequestValidator : AbstractValidator<SelectEspecialidadeGrupoByIdRequest>
{
    public SelectEspecialidadeGrupoByIdRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Grupo da Especialidade");
    }
}
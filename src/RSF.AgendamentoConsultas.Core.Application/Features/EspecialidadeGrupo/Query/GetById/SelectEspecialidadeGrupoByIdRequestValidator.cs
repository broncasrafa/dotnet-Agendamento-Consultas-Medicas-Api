using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetById;

public class SelectEspecialidadeGrupoByIdRequestValidator : AbstractValidator<SelectEspecialidadeGrupoByIdRequest>
{
    public SelectEspecialidadeGrupoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Grupo da Especialidade deve ser maior que 0");
    }
}
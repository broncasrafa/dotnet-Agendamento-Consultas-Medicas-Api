using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.GetById;

public class SelectEspecialidadeByIdRequestValidator : AbstractValidator<SelectEspecialidadeByIdRequest>
{
    public SelectEspecialidadeByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID da especialidade deve ser maior que 0");
    }
}
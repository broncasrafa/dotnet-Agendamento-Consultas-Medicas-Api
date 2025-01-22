using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithEspecialidades;

public class SelectEspecialistaByIdWithEspecialidadesRequestValidator : AbstractValidator<SelectEspecialistaByIdWithEspecialidadesRequest>
{
    public SelectEspecialistaByIdWithEspecialidadesRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");
    }
}
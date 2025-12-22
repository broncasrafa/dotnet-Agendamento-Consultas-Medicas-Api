using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithEspecialidades;

public class SelectEspecialistaByIdWithEspecialidadesRequestValidator : AbstractValidator<SelectEspecialistaByIdWithEspecialidadesRequest>
{
    public SelectEspecialistaByIdWithEspecialidadesRequestValidator()
    {
        RuleFor(x => x.Id).IdValidators("Especialista");
    }
}
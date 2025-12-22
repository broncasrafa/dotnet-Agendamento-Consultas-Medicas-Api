using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Search.Query.GetByNameWithEspecialistas;

public class SelectEspecialidadesAndEspecialistasByNameRequestValidator : AbstractValidator<SelectEspecialidadesAndEspecialistasByNameRequest>
{
    public SelectEspecialidadesAndEspecialistasByNameRequestValidator()
    {
        RuleFor(c => c.Term)
            .NotNullOrEmptyValidators("termo da pesquisa", minLength: 3)
            .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O termo da pesquisa deve conter apenas letras.");
    }
}
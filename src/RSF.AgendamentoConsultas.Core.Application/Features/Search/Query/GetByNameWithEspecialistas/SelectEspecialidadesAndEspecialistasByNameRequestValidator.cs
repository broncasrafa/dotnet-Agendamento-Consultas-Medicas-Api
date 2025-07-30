using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Search.Query.GetByNameWithEspecialistas;

public class SelectEspecialidadesAndEspecialistasByNameRequestValidator : AbstractValidator<SelectEspecialidadesAndEspecialistasByNameRequest>
{
    public SelectEspecialidadesAndEspecialistasByNameRequestValidator()
    {
        RuleFor(c => c.Term)
            .NotEmpty().WithMessage("O termo da pesquisa é obrigatória, não deve ser nulo ou vazio")
            .MinimumLength(3).WithMessage("O termo da pesquisa deve ter no mínimo 3 caracteres")
            .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("O termo da pesquisa deve conter apenas letras.");
    }
}
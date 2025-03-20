using System.Xml;

using FluentValidation;

using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetEspecialistasFavoritos;

public class SelectEspecialistasFavoritosPacienteRequestValidator : AbstractValidator<SelectEspecialistasFavoritosPacienteRequest>
{
    public SelectEspecialistasFavoritosPacienteRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => TypeValids.VALID_PAGE_SIZES.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser maior que zero.");
    }
}
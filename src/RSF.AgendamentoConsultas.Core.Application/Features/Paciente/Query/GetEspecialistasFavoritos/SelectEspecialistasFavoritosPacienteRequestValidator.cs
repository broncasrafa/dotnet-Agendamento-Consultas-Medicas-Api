using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetEspecialistasFavoritos;

public class SelectEspecialistasFavoritosPacienteRequestValidator : AbstractValidator<SelectEspecialistasFavoritosPacienteRequest>
{
    public SelectEspecialistasFavoritosPacienteRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
    }
}
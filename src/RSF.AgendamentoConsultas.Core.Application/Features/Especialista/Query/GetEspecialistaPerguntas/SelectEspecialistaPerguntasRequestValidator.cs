using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaPerguntas;

public class SelectEspecialistaPerguntasRequestValidator : AbstractValidator<SelectEspecialistaPerguntasRequest>
{
    public SelectEspecialistaPerguntasRequestValidator()
    {
        RuleFor(x => x.PageSize).PaginatedFieldValidators("PageSize");
        RuleFor(x => x.PageNum).PaginatedFieldValidators("PageNum");
    }
}
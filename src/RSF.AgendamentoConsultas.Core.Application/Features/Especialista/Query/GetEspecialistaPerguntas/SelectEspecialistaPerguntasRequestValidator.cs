using RSF.AgendamentoConsultas.Core.Domain.Validation;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaPerguntas;

public class SelectEspecialistaPerguntasRequestValidator : AbstractValidator<SelectEspecialistaPerguntasRequest>
{
    public SelectEspecialistaPerguntasRequestValidator()
    {
        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0).WithMessage("O Id do Especialista deve ser que zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => TypeValids.VALID_PAGE_SIZES.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser que zero.");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetAllPaged;

public class SelectPerguntaPagedRequestValidator : AbstractValidator<SelectPerguntaPagedRequest>
{
    private static readonly int[] _validPageSizes = [5, 10, 20, 50, 100];

    public SelectPerguntaPagedRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => _validPageSizes.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser maior que zero.");
    }
}
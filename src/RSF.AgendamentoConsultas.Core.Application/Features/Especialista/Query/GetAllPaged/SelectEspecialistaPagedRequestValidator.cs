using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetAllPaged;

public class SelectEspecialistaPagedRequestValidator : AbstractValidator<SelectEspecialistaPagedRequest>
{
    private static readonly int[] validPageSizes = [5, 10, 20, 50, 100];

    public SelectEspecialistaPagedRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => validPageSizes.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser maior que zero.");
    }
}
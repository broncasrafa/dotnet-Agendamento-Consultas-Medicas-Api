using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByNamePaged;

public class SelectEspecialistaByNamePagedRequestValidator : AbstractValidator<SelectEspecialistaByNamePagedRequest>
{
    private static readonly int[] validPageSizes = [5, 10, 20, 50, 100];

    public SelectEspecialistaByNamePagedRequestValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize deve ser maior que zero.")
            .Must(pageSize => validPageSizes.Contains(pageSize)).WithMessage("PageSize deve ser 5, 10, 20, 50 ou 100.");

        RuleFor(x => x.PageNum)
            .GreaterThan(0).WithMessage("PageNum deve ser que zero.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome não pode ser nulo ou vazio.")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos 2 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("O nome deve conter apenas letras e espaços.");
    }
}
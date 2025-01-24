using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("O Id do usuário é obrigatório, não deve ser nulo ou vazio");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("O Código de confirmação é obrigatório, não deve ser nulo ou vazio");
    }
}
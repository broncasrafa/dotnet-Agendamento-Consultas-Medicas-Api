using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginPaciente;

public class LoginPacienteRequestValidator : AbstractValidator<LoginPacienteRequest>
{
    public LoginPacienteRequestValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Senha é obrigatório, não deve ser nulo ou vazia");
    }
}
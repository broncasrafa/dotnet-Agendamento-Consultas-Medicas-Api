using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;

public class UpdateAuthenticatedUserInfoRequestValidator : AbstractValidator<UpdateAuthenticatedUserInfoRequest>
{
    public UpdateAuthenticatedUserInfoRequestValidator()
    {
        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Username).NotNullOrEmptyValidators("Username", minLength: 6);
    }
}
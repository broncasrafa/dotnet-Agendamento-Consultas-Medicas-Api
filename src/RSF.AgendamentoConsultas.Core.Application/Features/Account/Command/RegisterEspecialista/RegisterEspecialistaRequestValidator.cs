using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestValidator : AbstractValidator<RegisterEspecialistaRequest>
{
    public RegisterEspecialistaRequestValidator()
    {
        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("O Username é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(6).WithMessage("O Username deve ter no mínimo 6 caracteres");

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.Licenca)
            .NotEmpty().WithMessage("A Licença é obrigatória, não deve ser nulo ou vazia")
            .MinimumLength(5).WithMessage("A Licença deve ter no mínimo 5 caracteres");

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida"); 
    }
}
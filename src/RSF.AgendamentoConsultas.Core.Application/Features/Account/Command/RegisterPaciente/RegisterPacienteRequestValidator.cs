using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;

public class RegisterPacienteRequestValidator : AbstractValidator<RegisterPacienteRequest>
{
    public RegisterPacienteRequestValidator()
    {
        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.CPF).Cascade(CascadeMode.Stop)
            .CpfValidations();

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("O Username é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(6).WithMessage("O Username deve ter no mínimo 6 caracteres");

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.DataNascimento).Cascade(CascadeMode.Stop)
            .DateOfBirthValidations();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida");        
    }
}
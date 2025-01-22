using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.CreatePaciente;

public class CreatePacienteRequestValidator : AbstractValidator<CreatePacienteRequest>
{
    public CreatePacienteRequestValidator()
    {
        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("paciente");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.DataNascimento).Cascade(CascadeMode.Stop)
            .DateOfBirthValidations();

        RuleFor(x => x.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.CPF).Cascade(CascadeMode.Stop)
            .CpfValidations();

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida");
    }
}
using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private static readonly string[] ValidTiposAcesso = { "ADMINISTRADOR", "CONSULTOR" };

    public RegisterRequestValidator()
    {
        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.Username).NotNullOrEmptyValidators("Username", minLength: 6);

        RuleFor(c => c.CPF).Cascade(CascadeMode.Stop)
            .CpfValidations();

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();
        
        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida");

        RuleFor(c => c.TipoAcesso).Cascade(CascadeMode.Stop)
            .NotNullOrEmptyValidators("Tipo de Acesso")
            .Must(tipoAcesso => ValidTiposAcesso.Contains(tipoAcesso?.ToUpperInvariant()))
            .WithMessage("O Tipo de Acesso deve ser 'Administrador' ou 'Consultor'.");
    }
}
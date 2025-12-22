using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestValidator : AbstractValidator<RegisterEspecialistaRequest>
{
    private string[] VALID_CATEGORIAS = ["BASIC", "PREMIUM"];

    public RegisterEspecialistaRequestValidator()
    {
        RuleFor(c => c.EspecialidadeId).Cascade(CascadeMode.Stop)
                .IdValidators("Especialidade");

        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.Username).Cascade(CascadeMode.Stop)
            .NotNullOrEmptyValidators("Username", minLength: 6);

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.TipoCategoria).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O Tipo de categoria é obrigatório, não deve ser nulo ou vazio")
            .Must(tipoCategoria => VALID_CATEGORIAS.Contains(tipoCategoria?.ToUpperInvariant()))
            .WithMessage("O Tipo de categoria deve ser 'Basic' ou 'Premium'.");

        RuleFor(c => c.Licenca).Cascade(CascadeMode.Stop)
            .NotNullOrEmptyValidators("Licença", minLength: 5);

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ConfirmPassword).Cascade(CascadeMode.Stop)
            .PasswordConfirmationValidations()
            .Equal(c => c.Password).WithMessage("Senha de confirmação não confere com a senha escolhida"); 
    }
}
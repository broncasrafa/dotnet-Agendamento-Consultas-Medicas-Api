using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestValidator : AbstractValidator<RegisterEspecialistaRequest>
{
    private string[] VALID_CATEGORIAS = ["BASIC", "PREMIUM"];

    public RegisterEspecialistaRequestValidator()
    {
        RuleFor(c => c.EspecialidadeId).Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("O ID do Especialidade deve ser maior que 0");

        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("usuário");

        RuleFor(c => c.Username).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O Username é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(6).WithMessage("O Username deve ter no mínimo 6 caracteres");

        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.TipoCategoria).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O Tipo de categoria é obrigatório, não deve ser nulo ou vazio")
            .Must(tipoCategoria => VALID_CATEGORIAS.Contains(tipoCategoria?.ToUpperInvariant()))
            .WithMessage("O Tipo de categoria deve ser 'Basic' ou 'Premium'.");

        RuleFor(c => c.Licenca).Cascade(CascadeMode.Stop)
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
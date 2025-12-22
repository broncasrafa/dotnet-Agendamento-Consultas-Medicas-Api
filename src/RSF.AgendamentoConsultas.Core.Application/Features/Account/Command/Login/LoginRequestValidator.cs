using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    //private static readonly string[] ValidTiposAcesso = { "PACIENTE", "PROFISSIONAL", "ADMINISTRADOR", "CONSULTOR" };

    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotNullOrEmptyValidators("Senha");

        //RuleFor(x => x.TipoAcesso)
        //    .NotEmpty().WithMessage("O Tipo de Acesso é obrigatório, não deve ser nulo ou vazio")
        //    .Must(tipoAcesso => ValidTiposAcesso.Contains(tipoAcesso?.ToUpperInvariant()))
        //    .WithMessage("O Tipo de Acesso deve ser 'Paciente', 'Profissional', 'Administrador' ou 'Consultor'.");
    }
}
using RSF.AgendamentoConsultas.Shareable.Helpers;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Shareable.Validations.Validators;

public static class TelefoneValidator
{
    public static IRuleBuilderOptions<T, string> TelefoneValidators<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("O telefone não pode ser nulo ou vazio.")
            .Matches(@"^\d+$").WithMessage("O telefone deve conter apenas números.")
            .Must(DeveTerNumerosTamanhosValidos).WithMessage("O telefone deve ter 10 dígitos para fixo ou 11 dígitos para celular.")
            .Must(NaoDeveTerNumerosConsecutivosIguais).WithMessage("O telefone não deve ter somente números consecutivos iguais");

    private static bool NaoDeveTerNumerosConsecutivosIguais(string numeroTelefone) 
        => numeroTelefone.RemoverFormatacaoSomenteNumeros().Distinct().Count() > 1;

    private static bool DeveTerNumerosTamanhosValidos(string numeroTelefone)
        => numeroTelefone.RemoverFormatacaoSomenteNumeros().Length == 10 || numeroTelefone.RemoverFormatacaoSomenteNumeros().Length == 11;
}
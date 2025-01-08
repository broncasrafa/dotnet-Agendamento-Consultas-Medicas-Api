using FluentValidation;

namespace RSF.AgendamentoConsultas.Shareable.Validations.Validators;

public static class NomeCompletoValidator
{
    public static IRuleBuilderOptions<T, string> NomeCompletoValidators<T>(this IRuleBuilder<T, string> ruleBuilder, string field)
        => ruleBuilder
            .NotEmpty().WithMessage($"O nome completo do {field} é obrigatório")
            .Must(DeveTerSobrenome).WithMessage($"O nome completo do {field} deve conter pelo menos um sobrenome.");
        

    private static bool DeveTerSobrenome(string nome)
        => nome.Trim().Contains(' ');
}
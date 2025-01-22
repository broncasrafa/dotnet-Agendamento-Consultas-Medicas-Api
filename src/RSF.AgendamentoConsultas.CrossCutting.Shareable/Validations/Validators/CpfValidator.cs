using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using FluentValidation;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;

public static class CpfValidator
{
    public static IRuleBuilderOptions<T, string> CpfValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty().WithMessage("O CPF é obrigatório, não deve ser nulo ou vazio")
                .Must(BeAValidCpfLength).WithMessage("O CPF deve conter 11 caracteres")
                .Must(Utilitarios.IsCpfValid).WithMessage("O CPF é inválido");
    }


    private static bool BeAValidCpfLength(string cpf)
    {
        cpf = cpf.RemoverFormatacaoSomenteNumeros();
        if (cpf != null && cpf.Length == 11) return true;
        return false;
    }
}
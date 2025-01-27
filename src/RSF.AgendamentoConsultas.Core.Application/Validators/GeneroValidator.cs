using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class GeneroValidator
{
    public static IRuleBuilderOptions<T, string> GeneroValidators<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty().WithMessage("O gênero não pode ser nulo ou vazio.")
            .Must(genero => 
                    genero.Equals("Masculino", StringComparison.InvariantCultureIgnoreCase) || 
                    genero.Equals("Feminino", StringComparison.InvariantCultureIgnoreCase) ||
                    genero.Equals("Não informado", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage("O gênero deve ser 'Masculino' ou 'Feminino'.")
        ;
}
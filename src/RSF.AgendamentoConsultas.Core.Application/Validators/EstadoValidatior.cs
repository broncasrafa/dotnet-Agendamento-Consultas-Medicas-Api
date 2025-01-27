using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class EstadoValidatior
{
    public static IRuleBuilderOptions<T, string> UfValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("UF do Estado é obrigatório")
            .Length(2).WithMessage("UF do Estado deve conter exatamente 2 caracteres")
            .Must(BeAValidUf).WithMessage("UF do Estado deve ser uma UF válida");
    }


    private static bool BeAValidUf(string uf)
    {
        string[] validUfs =
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
            "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
            "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };
        return validUfs.Any(c => c.Equals(uf, StringComparison.OrdinalIgnoreCase));
    }
}
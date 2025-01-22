using FluentValidation;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;

public static class DateOfBirthValidator 
{
    public static IRuleBuilderOptions<T, DateTime> DateOfBirthValidations<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Data de nascimento é obrigatório")
            .Must(BeAValidDate).WithMessage("Data de nascimento deve ser uma data valida")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Data de nascimento deve ser menor ou igual a data atual");
    }

    private static bool BeAValidDate(DateTime date)
        => date != default(DateTime);
}

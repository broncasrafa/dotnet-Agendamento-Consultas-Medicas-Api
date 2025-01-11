using RSF.AgendamentoConsultas.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependente;

public class UpdatePacienteDependenteRequestValidator : AbstractValidator<UpdatePacienteDependenteRequest>
{
    public UpdatePacienteDependenteRequestValidator()
    {
        RuleFor(x => x.DependenteId)
            .GreaterThan(0).WithMessage("O ID do Dependente deve ser maior que 0");

        RuleFor(x => x.PacientePrincipalId)
            .GreaterThan(0).WithMessage("O ID do Paciente Principal deve ser maior que 0");

        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators("dependente");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.DataNascimento).Cascade(CascadeMode.Stop)
            .DateOfBirthValidations();

        RuleFor(x => x.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.CPF).Cascade(CascadeMode.Stop)
            .CpfValidations();

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();
    }
}
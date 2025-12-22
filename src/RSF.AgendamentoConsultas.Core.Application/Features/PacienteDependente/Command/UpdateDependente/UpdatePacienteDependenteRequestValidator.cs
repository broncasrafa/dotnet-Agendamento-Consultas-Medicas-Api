using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependente;

public class UpdatePacienteDependenteRequestValidator : AbstractValidator<UpdatePacienteDependenteRequest>
{
    public UpdatePacienteDependenteRequestValidator()
    {
        RuleFor(x => x.DependenteId).IdValidators("Dependente");
        RuleFor(x => x.PacientePrincipalId).IdValidators("Paciente Principal");

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
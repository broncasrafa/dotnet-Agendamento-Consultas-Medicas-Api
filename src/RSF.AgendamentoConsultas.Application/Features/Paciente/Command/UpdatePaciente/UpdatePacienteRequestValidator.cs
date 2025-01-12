using RSF.AgendamentoConsultas.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.UpdatePaciente;

public class UpdatePacienteRequestValidator : AbstractValidator<UpdatePacienteRequest>
{
    public UpdatePacienteRequestValidator()
    {
        RuleFor(x => x.PacienteId)
        .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators(field: "paciente");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.DataNascimento).Cascade(CascadeMode.Stop)
            .DateOfBirthValidations();

        RuleFor(x => x.Genero).Cascade(CascadeMode.Stop)
            .GeneroValidators();

        RuleFor(c => c.Telefone).Cascade(CascadeMode.Stop)
            .TelefoneValidators();
    }
}
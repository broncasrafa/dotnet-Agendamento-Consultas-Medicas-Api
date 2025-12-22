using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePaciente;

public class UpdatePacienteRequestValidator : AbstractValidator<UpdatePacienteRequest>
{
    public UpdatePacienteRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");

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
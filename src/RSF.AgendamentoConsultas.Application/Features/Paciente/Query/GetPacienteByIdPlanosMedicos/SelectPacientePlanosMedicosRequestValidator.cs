using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public class SelectPacientePlanosMedicosRequestValidator : AbstractValidator<SelectPacientePlanosMedicosRequest>
{
    public SelectPacientePlanosMedicosRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
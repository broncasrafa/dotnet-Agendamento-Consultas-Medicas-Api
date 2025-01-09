using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestValidator : AbstractValidator<SelectPacienteByIdRequest>
{
    public SelectPacienteByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
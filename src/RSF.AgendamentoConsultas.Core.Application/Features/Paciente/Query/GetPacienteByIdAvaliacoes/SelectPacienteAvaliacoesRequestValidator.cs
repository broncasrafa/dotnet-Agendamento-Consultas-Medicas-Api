using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;

public class SelectPacienteAvaliacoesRequestValidator : AbstractValidator<SelectPacienteAvaliacoesRequest>
{
    public SelectPacienteAvaliacoesRequestValidator()
    {
        RuleFor(x => x.PacienteId)
           .GreaterThan(0)
           .WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
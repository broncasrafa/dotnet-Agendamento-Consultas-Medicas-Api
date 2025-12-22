using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;

public class SelectPacienteAvaliacoesRequestValidator : AbstractValidator<SelectPacienteAvaliacoesRequest>
{
    public SelectPacienteAvaliacoesRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
    }
}
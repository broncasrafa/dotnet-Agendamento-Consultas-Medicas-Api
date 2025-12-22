using FluentValidation;

using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddPerguntaEspecialista;

public class AddPerguntaEspecialistaRequestValidator : AbstractValidator<AddPerguntaEspecialistaRequest>
{
    public AddPerguntaEspecialistaRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(c => c.Pergunta).NotNullOrEmptyValidators("texto da Pergunta", minLength: 5, maxLength: 300);
        RuleFor(x => x.TermosUsoPolitica)
            .Equal(true).WithMessage("Você deve aceitar os Termos de Uso e Política de Privacidade.");
    }
}
using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Command.CreatePergunta;

public class CreatePerguntaRequestValidator : AbstractValidator<CreatePerguntaEspecialidadeRequest>
{
    public CreatePerguntaRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.EspecialidadeId).IdValidators("Especialidade");
        RuleFor(c => c.Pergunta)
            .NotNullOrEmptyValidators("texto da Pergunta", minLength: 5, maxLength: 300);
        RuleFor(x => x.TermosUsoPolitica)
            .Equal(true).WithMessage("Você deve aceitar os Termos de Uso e Política de Privacidade.");
    }
}
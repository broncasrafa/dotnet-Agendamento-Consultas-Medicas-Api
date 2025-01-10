using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Command.CreatePergunta;

public class CreatePerguntaRequestValidator : AbstractValidator<CreatePerguntaRequest>
{
    public CreatePerguntaRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.Titulo)
            .NotEmpty().WithMessage("O título da pergunta é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O título da pergunta deve ter pelo menos 5 caracteres");

        RuleFor(c => c.Pergunta)
            .NotEmpty().WithMessage("O texto da pergunta é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O texto da pergunta deve ter pelo menos 5 caracteres");
    }
}
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddPerguntaEspecialista;

public class AddPerguntaEspecialistaRequestValidator : AbstractValidator<CreatePerguntaEspecialistaRequest>
{
    public AddPerguntaEspecialistaRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0)
            .WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0)
            .WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.Pergunta)
            .NotEmpty().WithMessage("O texto da Pergunta é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O texto da Pergunta deve ter pelo menos 5 caracteres")
            .MaximumLength(300).WithMessage("O texto da Pergunta deve ter no máximo 300 caracteres");

        RuleFor(x => x.TermosUsoPolitica)
            .Equal(true).WithMessage("Você deve aceitar os Termos de Uso e Política de Privacidade.");
    }
}
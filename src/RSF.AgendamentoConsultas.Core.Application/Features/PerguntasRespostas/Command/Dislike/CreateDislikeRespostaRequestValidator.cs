using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Dislike;

public class CreateDislikeRespostaRequestValidator : AbstractValidator<CreateDislikeRespostaRequest>
{
    public CreateDislikeRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId)
            .NotEmpty().WithMessage("O ID da Resposta é obrigatório.")
            .GreaterThan(0).WithMessage("O ID da Resposta deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .NotEmpty().WithMessage("O ID do Paciente é obrigatório.")
            .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
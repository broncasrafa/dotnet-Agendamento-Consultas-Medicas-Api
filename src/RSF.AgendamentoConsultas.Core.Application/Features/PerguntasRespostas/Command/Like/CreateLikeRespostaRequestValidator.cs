using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Like;

public class CreateLikeRespostaRequestValidator :  AbstractValidator<CreateLikeRespostaRequest>
{
    public CreateLikeRespostaRequestValidator()
    {
        RuleFor(x => x.RespostaId)
            .NotEmpty().WithMessage("O ID da Resposta é obrigatório.")
            .GreaterThan(0).WithMessage("O ID da Resposta deve ser maior que 0");

        RuleFor(x => x.PacienteId)
            .NotEmpty().WithMessage("O ID do Paciente é obrigatório.")
            .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");
    }
}
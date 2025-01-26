using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;

public class CreateAvaliacaoRequestValidator : AbstractValidator<CreateAvaliacaoRequest>
{
    public CreateAvaliacaoRequestValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0).WithMessage("O ID do Paciente deve ser maior que 0");

        RuleFor(x => x.EspecialistaId)
            .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.Feedback)
            .NotEmpty().WithMessage("O Feedback da avaliação é obrigatório, não deve ser nulo ou vazio")
            .MinimumLength(5).WithMessage("O Feedback da avaliação deve ter pelo menos 5 caracteres");

        RuleFor(c => c.Score)
            .InclusiveBetween(1, 5).WithMessage("O Score da avaliação deve ser um valor entre 1 e 5");

        RuleFor(c => c.TagId)
            .GreaterThan(0).When(c => c.TagId.HasValue).WithMessage("O ID da Tag deve ser maior que 0");
    }
}
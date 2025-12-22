using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;

public class CreateAvaliacaoRequestValidator : AbstractValidator<CreateAvaliacaoRequest>
{
    public CreateAvaliacaoRequestValidator()
    {
        RuleFor(x => x.PacienteId).IdValidators("Paciente");
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");
        RuleFor(c => c.Feedback).NotNullOrEmptyValidators("Feedback da avaliação", minLength: 5);
        RuleFor(c => c.TagId).IdValidators("Tag", c => c.TagId.HasValue);
        RuleFor(c => c.Score)
            .InclusiveBetween(1, 5).WithMessage("O Score da avaliação deve ser um valor entre 1 e 5");
    }
}
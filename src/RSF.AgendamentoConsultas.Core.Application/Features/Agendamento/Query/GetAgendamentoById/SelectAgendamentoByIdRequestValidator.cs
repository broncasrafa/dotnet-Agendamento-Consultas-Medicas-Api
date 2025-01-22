using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Query.GetAgendamentoById;

public class SelectAgendamentoByIdRequestValidator : AbstractValidator<SelectAgendamentoByIdRequest>
{
    public SelectAgendamentoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Agendamento deve ser maior que 0");
    }
}
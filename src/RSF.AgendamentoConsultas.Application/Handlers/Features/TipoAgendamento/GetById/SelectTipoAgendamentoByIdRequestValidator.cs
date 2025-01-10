using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.GetById;

public class SelectTipoAgendamentoByIdRequestValidator : AbstractValidator<SelectTipoAgendamentoByIdRequest>
{
    public SelectTipoAgendamentoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Agendamento deve ser maior que 0");
    }
}
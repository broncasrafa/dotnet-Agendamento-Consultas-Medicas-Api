using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.GetById;

public class SelectTipoAgendamentoByIdRequestValidator : AbstractValidator<SelectTipoAgendamentoByIdRequest>
{
    public SelectTipoAgendamentoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Agendamento deve ser maior que 0");
    }
}
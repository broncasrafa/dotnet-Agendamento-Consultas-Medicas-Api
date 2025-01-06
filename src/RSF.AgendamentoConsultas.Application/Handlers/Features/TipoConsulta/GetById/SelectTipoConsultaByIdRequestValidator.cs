using FluentValidation;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetById;

public class SelectTipoConsultaByIdRequestValidator : AbstractValidator<SelectTipoConsultaByIdRequest>
{
    public SelectTipoConsultaByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Consulta deve ser maior que 0");
    }
}
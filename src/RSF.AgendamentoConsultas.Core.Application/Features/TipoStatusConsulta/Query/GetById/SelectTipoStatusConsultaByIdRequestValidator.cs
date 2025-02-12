using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Query.GetById;

public class SelectTipoStatusConsultaByIdRequestValidator : AbstractValidator<SelectTipoStatusConsultaByIdRequest>
{
    public SelectTipoStatusConsultaByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("O ID do Tipo de Status da Consulta deve ser maior que 0");
    }
}
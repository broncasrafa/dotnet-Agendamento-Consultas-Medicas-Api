using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialista;

public class UpdateEspecialistaRequestValidator : AbstractValidator<UpdateEspecialistaRequest>
{
    private string[] VALID_CATEGORIAS = ["BASIC", "PREMIUM"];

    public UpdateEspecialistaRequestValidator()
    {
        RuleFor(c => c.EspecialistaId)
                .GreaterThan(0).WithMessage("O ID do Especialista deve ser maior que 0");

        RuleFor(c => c.NomeCompleto).Cascade(CascadeMode.Stop)
            .NomeCompletoValidators(field: "Especialista");

        RuleFor(c => c.Tipo).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O Tipo de categoria é obrigatório, não deve ser nulo ou vazio")
            .Must(tipoCategoria => VALID_CATEGORIAS.Contains(tipoCategoria?.ToUpperInvariant()))
            .WithMessage("O Tipo de categoria deve ser 'Basic' ou 'Premium'.");

        RuleFor(c => c.Foto)
            .Must(link => string.IsNullOrEmpty(link) || IsValidUrl(link))
                .WithMessage("O link da foto deve ser uma URL válida começando com 'http://' ou 'https://'.");

        RuleFor(x => x.TelemedicinaPreco)
                .ValorMonetarioValidations(field: "TelemedicinaPreco");

        RuleFor(c => c.ExperienciaProfissional)
                .MinimumLength(15).WithMessage("A Experiência Profissional deve ter no mínimo 15 caracteres")
                .MaximumLength(1000).WithMessage("A Experiência Profissional deve ter no máximo 1000 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.ExperienciaProfissional));

        RuleFor(c => c.FormacaoAcademica)
                .MinimumLength(15).WithMessage("A Formação Acadêmica deve ter no mínimo 15 caracteres")
                .MaximumLength(1000).WithMessage("A Formação Acadêmica deve ter no máximo 1000 caracteres")
                .When(x => !string.IsNullOrWhiteSpace(x.FormacaoAcademica));
    }


    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

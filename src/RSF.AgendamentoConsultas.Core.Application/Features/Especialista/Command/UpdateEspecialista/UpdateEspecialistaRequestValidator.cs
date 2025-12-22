using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialista;

public class UpdateEspecialistaRequestValidator : AbstractValidator<UpdateEspecialistaRequest>
{
    private string[] VALID_CATEGORIAS = ["BASIC", "PREMIUM"];

    public UpdateEspecialistaRequestValidator()
    {
        RuleFor(x => x.EspecialistaId).IdValidators("Especialista");

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
            .NotNullOrEmptyValidators("Experiência Profissional", minLength: 15, maxLength: 1000, predicate: x => !string.IsNullOrWhiteSpace(x.ExperienciaProfissional));

        RuleFor(c => c.FormacaoAcademica)
            .NotNullOrEmptyValidators("Formação Acadêmica", minLength: 15, maxLength: 1000, predicate: x => !string.IsNullOrWhiteSpace(x.FormacaoAcademica));
    }


    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

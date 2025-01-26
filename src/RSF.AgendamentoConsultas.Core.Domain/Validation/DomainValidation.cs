using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using RSF.AgendamentoConsultas.Core.Domain.Exceptions;

namespace RSF.AgendamentoConsultas.Core.Domain.Validation;

public static class DomainValidation
{
    public static void NotNullOrEmpty(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");
    }

    public static void IdentifierGreaterThanZero(int? identifierValue, string fieldName)
    {
        if (identifierValue.HasValue && identifierValue <= 0)
            throw new EntityValidationException($"{fieldName} deve ser maior que zero.");
    }

    public static void PossiblesValidTypes(string[] validTypes, string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        if (validTypes is null || validTypes.Length == 0)
            throw new EntityValidationException($"A lista de tipos validos não pode ser nulo ou vazio.");

        var isValid = Array.Exists(validTypes, type => type.Equals(value, StringComparison.OrdinalIgnoreCase));

        if (!isValid)
            throw new EntityValidationException($"{fieldName} inválido. Valores válidos: '{string.Join(", ", validTypes)}'");
    }
    public static void PossiblesValidTypes(int[] validTypes, int? value, string fieldName)
    {
        if (!value.HasValue)
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        if (validTypes is null || validTypes.Length == 0)
            throw new EntityValidationException($"A lista de tipos validos não pode ser nulo ou vazio.");

        var isValid = Array.Exists(validTypes, type => type == value.Value);

        if (!isValid)
            throw new EntityValidationException($"{fieldName} inválido. Valores válidos: '{string.Join(", ", validTypes)}'");
    }

    public static void PossibleValidDate(string value, bool permitirSomenteDatasFuturas, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        const string pattern = "yyyy-MM-dd";

        // Verifica se a data está no formato esperado
        if (!DateTime.TryParseExact(value, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            throw new EntityValidationException($"{fieldName} está em um formato inválido. O formato correto é {pattern}.");

        // Validação de data futura ou passada
        if (permitirSomenteDatasFuturas)
        {
            if (parsedDate <= DateTime.Today)
                throw new EntityValidationException($"{fieldName} deve ser uma data futura e maior que hoje.");
        }
        else
        {
            if (parsedDate >= DateTime.Today)
                throw new EntityValidationException($"{fieldName} deve ser uma data passada e menor que hoje.");
        }
    }

    public static void PossibleValidPhoneNumber(string value, string fieldName, bool isRequired = true)
    {
        // Se o telefone é obrigatório e não foi informado
        if (isRequired && string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        // Se não é obrigatório e o valor foi informado, faz a validação
        if (!string.IsNullOrWhiteSpace(value))
        {
            // Remove formatações
            string onlyNumbers = Regex.Replace(value, @"[^\d]", "");

            // Verifica se contém apenas números
            if (!Regex.IsMatch(onlyNumbers, @"^\d+$"))
                throw new EntityValidationException($"{fieldName} deve conter apenas números.");

            // Valida a quantidade de dígitos
            if (onlyNumbers.Length < 10 || onlyNumbers.Length > 11)
                throw new EntityValidationException($"{fieldName} deve conter 10 (fixo) ou 11 (celular) dígitos.");
        }
    }

    public static void PriceGreaterThanZero(decimal? value, string fieldName)
    {
        if (value.HasValue && value <= 0)
            throw new EntityValidationException($"{fieldName} deve ser maior que zero.");
    }

    public static void PossibleValidNumber(string value, string fieldName)
    {
        if (!string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"^\d+$"))
        {
            throw new EntityValidationException($"{fieldName} deve conter apenas números.");
        }
    }

    public static void PossibleValidTime(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        // Define o formato esperado de hora
        string pattern = "HH:mm";

        // Tenta converter a string para o formato "HH:mm"
        if (!DateTime.TryParseExact(value, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
            throw new EntityValidationException($"{fieldName} deve estar no formato válido HH:mm.");

        // Verifica se a hora e minutos estão dentro do intervalo esperado
        if (parsedTime.Hour < 0 || parsedTime.Hour > 23 || parsedTime.Minute < 0 || parsedTime.Minute > 59)
            throw new EntityValidationException($"{fieldName} deve ser uma hora válida no formato HH:mm.");
    }

    public static void PossibleValidPassword(string senha, int minimumLength = 5)
    {
        if (!string.IsNullOrWhiteSpace(senha))
        {
            var erros = new List<string>();

            if (senha.Length < minimumLength)
                erros.Add($"Senha deve ter pelo menos {minimumLength} caracteres");

            if (!Regex.IsMatch(senha, "[A-Z]"))
                erros.Add("Senha deve ter pelo menos 1 letra maiúscula");

            if (!Regex.IsMatch(senha, "[a-z]"))
                erros.Add("Senha deve ter pelo menos 1 letra minúscula");

            if (!Regex.IsMatch(senha, "[0-9]"))
                erros.Add("Senha deve ter pelo menos 1 número");

            if (!Regex.IsMatch(senha, "[!*@#$%^&+=]"))
                erros.Add("Senha deve ter pelo menos 1 caractere especial");

            if (erros.Count > 0)
                throw new EntityValidationException($"A senha não atende aos critérios: {string.Join(", ", erros)}");
        }
    }

    public static void PossibleValidEmailAddress(string value, string fieldName, bool isRequired = true)
    {
        if (isRequired && string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        // Se não é obrigatório e o valor foi informado, faz a validação
        if (!string.IsNullOrWhiteSpace(value))
        {
            var emailAttribute = new EmailAddressAttribute();
            var isValid = emailAttribute.IsValid(value);
            if (!isValid)
                throw new EntityValidationException($"{fieldName} com valor inválido.");
        }
    }

    public static void PossibleValidFullName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{fieldName} não pode ser nulo ou vazio.");

        if (!value.Trim().Contains(' '))
            throw new EntityValidationException($"{fieldName} inválido.");
    }

    public static void PossibleValidUF(string value, string fieldName)
    {
        if (value is not null)
        {
            if (value.Length != 2)
                throw new EntityValidationException($"{fieldName} deve ter 2 caracteres.");

            if (!Regex.IsMatch(value, @"^[A-Z]+$"))
                throw new EntityValidationException($"{fieldName} deve conter apenas letras maiúsculas.");

            var validUFs = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };
            if (!validUFs.Contains(value))
                throw new EntityValidationException($"{fieldName} inválido. Valores válidos: '{string.Join(", ", validUFs)}'");
        }
    }
}
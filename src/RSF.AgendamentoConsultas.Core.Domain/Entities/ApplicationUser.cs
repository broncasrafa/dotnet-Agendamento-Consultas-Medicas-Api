using Microsoft.AspNetCore.Identity;
using RSF.AgendamentoConsultas.Core.Domain.Validation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; }
    public string Documento { get; set; }
    public string Genero { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public ApplicationUser(string nomeCompleto, string username, string documento, string email, string genero, string telefone)
    {
        NomeCompleto = nomeCompleto;
        UserName = username;
        Documento = documento;
        Email = email;
        Genero = genero;
        PhoneNumber = telefone.RemoverFormatacaoSomenteNumeros();
        CreatedAt = DateTime.Now;
        IsActive = true;

        Validate();
    }

    public void Update(string nomeCompleto, string telefone, string email, string username, bool isEmailChanged = false)
    {
        NomeCompleto = nomeCompleto;
        PhoneNumber = telefone.RemoverFormatacaoSomenteNumeros();
        Email = email;
        UserName = username;
        //NormalizedUserName = username.ToUpper();
        //NormalizedEmail = email.ToUpper();
        UpdatedAt = DateTime.Now;

        if (isEmailChanged) 
            EmailConfirmed = false;

        Validate();
    }

    void Validate()
    {
        DomainValidation.NotNullOrEmpty(Documento, nameof(Documento));

        DomainValidation.NotNullOrEmpty(NomeCompleto, nameof(NomeCompleto));
        DomainValidation.PossibleValidFullName(NomeCompleto, nameof(NomeCompleto));

        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.PossibleValidEmailAddress(Email, nameof(Email));

        DomainValidation.NotNullOrEmpty(UserName, nameof(UserName));
        
        DomainValidation.NotNullOrEmpty(PhoneNumber, nameof(PhoneNumber));
        DomainValidation.PossibleValidPhoneNumber(PhoneNumber, nameof(PhoneNumber));

        DomainValidation.NotNullOrEmpty(Genero, nameof(Genero));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
    }
}

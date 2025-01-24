using Microsoft.AspNetCore.Identity;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; }
    public string Documento { get; set; }
    public string Genero { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }


    public void Update(string nomeCompleto, string telefone, string email, string username, bool isEmailChanged = false)
    {
        Validate();

        NomeCompleto = nomeCompleto;
        PhoneNumber = telefone;
        Email = email;
        UserName = username;
        NormalizedUserName = username.ToUpper();
        NormalizedEmail = email.ToUpper();
        UpdatedAt = DateTime.Now;

        if (isEmailChanged) 
            EmailConfirmed = false;
    }

    void Validate()
    {
        DomainValidation.NotNullOrEmpty(NomeCompleto, nameof(NomeCompleto));
        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.NotNullOrEmpty(UserName, nameof(UserName));
        DomainValidation.NotNullOrEmpty(PhoneNumber, nameof(PhoneNumber));
        DomainValidation.PossibleValidPhoneNumber(PhoneNumber, nameof(PhoneNumber));
        DomainValidation.PossibleValidEmailAddress(Email, nameof(Email));
        DomainValidation.PossibleValidFullName(NomeCompleto, nameof(NomeCompleto));
    }
}

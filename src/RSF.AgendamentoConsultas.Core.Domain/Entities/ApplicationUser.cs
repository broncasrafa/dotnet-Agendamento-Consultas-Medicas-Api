using Microsoft.AspNetCore.Identity;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; }
    public string Documento { get; set; }
    public string Genero { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
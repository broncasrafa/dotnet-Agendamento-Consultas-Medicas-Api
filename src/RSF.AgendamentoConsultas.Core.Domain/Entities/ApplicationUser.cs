using Microsoft.AspNetCore.Identity;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
}
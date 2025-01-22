using System.Security.Claims;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateTokenJwt(ApplicationUser user, IList<Claim> roleClaims, IList<Claim> userClaims);
}
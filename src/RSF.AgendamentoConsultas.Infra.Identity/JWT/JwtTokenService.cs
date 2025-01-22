using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Infra.Identity.Configurations;

namespace RSF.AgendamentoConsultas.Infra.Identity.JWT;

public class JwtTokenService : IJwtTokenService
{
    private readonly ILogger<JwtTokenService> _logger;
    private readonly IOptions<JWTSettings> _options;

    public JwtTokenService(ILogger<JwtTokenService> logger, IOptions<JWTSettings> options)
    {
        _logger = logger;
        _options = options;
    }

    public string GenerateTokenJwt(ApplicationUser user, IReadOnlyList<Claim> roleClaims, IReadOnlyList<Claim> userClaims)
    {
        _logger.LogInformation($"Generating JWT token for user: '{user.UserName}'");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("userId", user.Id),
        }.Union(userClaims)
         .Union(roleClaims);

        try
        {
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_options.Value.Key));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_options.Value.DurationInDays),
                Issuer = _options.Value.Issuer,
                Audience = _options.Value.Audience,
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwt);
        }
        catch (Exception ex)
        {
            throw new JwtTokenGeneratorErrorException(ex.Message);
        }
    }
}
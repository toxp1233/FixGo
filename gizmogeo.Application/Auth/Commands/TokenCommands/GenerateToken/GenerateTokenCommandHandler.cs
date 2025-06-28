using gizmogeo.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.GenerateToken;

public class GenerateTokenCommandHandler(IConfiguration config) : IRequestHandler<GenerateTokenCommand, string>
{
    public Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenKey = config["TokenSettings:Key"] ?? throw new NotFoundException("Token","TokenKey");
        if (tokenKey.Length < 64) throw new Exception("Token key too short");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.User.Id.ToString()),
                new Claim(ClaimTypes.Role, request.User.Role.Name),
                new Claim(ClaimTypes.Name, request.User.Name),
                new Claim(ClaimTypes.MobilePhone, request.User.PhoneNumber),
                new Claim("Balance", request.User.Balance.ToString())
            };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = creds,
            Issuer = config["TokenSettings:Issuer"],
            Audience = config["TokenSettings:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return Task.FromResult(tokenString);
    }
}

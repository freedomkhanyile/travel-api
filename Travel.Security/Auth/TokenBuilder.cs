using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Travel.Security.Auth
{
    public class TokenBuilder:ITokenBuilder
    {
        public string Build(string name, string[] roles, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = roles
                .Select(userRole => new Claim(ClaimTypes.Role, userRole))
                .ToList();

            var identity = new ClaimsIdentity(
                new GenericIdentity(name, TokenAuthOption.TokenType),
                claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }
    }
}

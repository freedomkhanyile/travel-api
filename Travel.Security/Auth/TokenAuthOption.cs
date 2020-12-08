using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Travel.Security.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "TravelAudience";
        public static string Issuer { get; } = "TravelIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";
    }
}

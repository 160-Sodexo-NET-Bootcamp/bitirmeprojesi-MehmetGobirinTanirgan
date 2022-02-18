using Microsoft.IdentityModel.Tokens;
using PCS.Core.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PCS.Core.Utils.Concrete
{
    public static class TokenGenerator
    {
        public static string GenerateJwtToken(JwtSettings jwtSettings, ClaimsIdentity claimsIdentity, DateTime expireDate)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                NotBefore = DateTime.UtcNow,
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.JwtKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public static string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}

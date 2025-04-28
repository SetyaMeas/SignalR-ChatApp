using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ChatApp.Application.Commons.DTOs;
using ChatApp.Application.Commons.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Infrastucture.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string SecretKey;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            SecretKey =
                configuration["Jwt:Secret"]
                ?? throw new ArgumentNullException("[JWT Configuration Error] Secret key is missing");
            ;
        }

        public string CreateToken(TokenCredential tokenCredential)
        {
            var secretKey = Encoding.ASCII.GetBytes(SecretKey);
            var credential = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = tokenCredential.ToClaims(),
                Expires = tokenCredential.ExpiredAt,
                SigningCredentials = credential,
            };

            var handler = new JwtSecurityTokenHandler();
            var createToken = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(createToken);
        }
    }
}

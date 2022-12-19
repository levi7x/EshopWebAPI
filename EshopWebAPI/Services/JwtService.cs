using EshopWebAPI.Models;
using EshopWebAPI.Services.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EshopWebAPI.Services
{
    public class JwtService
    {
        private const int EXPIRATION_MINUTES = 5;

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateToken(User user, string role)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

            var token = CreateJwtToken(
                CreateClaims(user,role),
                CreateSigningCredentials(),
                expiration
            );
            
            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthenticationResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration
            };
        }
        
        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
            new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        
        private Claim[] CreateClaims(User user,string role) =>
            new[] {
                //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)            
            };

        private SigningCredentials CreateSigningCredentials() =>
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ),
                SecurityAlgorithms.HmacSha256
            );

        //private string secureKey = _configuration["Jwt:Key"];

        public string Generate(User user, string role)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(user.Id.ToString(), null, CreateClaims(user, role), null, DateTime.UtcNow.AddMinutes(1)); // 1 min
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}

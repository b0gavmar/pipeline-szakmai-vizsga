using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PipeLine.Backend.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string userId, string email, bool stayLoggedIn)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);
            var claims = new[]
            {
                //new Claim("id",userId),
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var audiences = _config.GetSection("JwtSettings:Audience").Get<string[]>();

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: audiences[2],
                claims: claims,
                expires: stayLoggedIn ?  DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(Convert.ToDouble(_config["JwtSettings:ExpirationHours"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TestAuthJWT
{
    public class JwtService
    {
        private readonly string _SECRETKEY = "my_secret_key_12345!@#$%^&*()123456789";
        public string GenerateToken(string username)
        {
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SECRETKEY));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7019",
                audience: "https://localhost:7019",
                claims: claim,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

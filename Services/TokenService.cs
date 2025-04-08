using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using E_Commerce_API.Models.Entities;

namespace E_Commerce_API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var chave = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(chave))
            {
                throw new InvalidOperationException("Chave JWT não configurada.");
            }

            if (string.IsNullOrEmpty(usuario.UserName) || string.IsNullOrEmpty(usuario.Id))
            {
                throw new InvalidOperationException("Usuário inválido para geração de token.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(chave);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, usuario.UserName),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

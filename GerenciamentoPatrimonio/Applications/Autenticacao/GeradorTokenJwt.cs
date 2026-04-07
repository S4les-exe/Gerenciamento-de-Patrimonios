using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciamentoPatrimonio.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            //var chave = _config["Jwt:Key"]!;
            var chave = Environment.GetEnvironmentVariable("JWT_KEY");

            if (string.IsNullOrWhiteSpace(chave))
            {
                throw new DomainException("JWT_KEY não configurada no .env");
            }

            var issuer = _config["Jwt:Issuer"]!;
            var audience = _config["Jwt:Audience"]!;
            var expiraEmMinutos = int.Parse(_config["Jwt:ExpiraEmMinutos"]!);

            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if (keyBytes.Length < 32)
            {
                throw new DomainException("Jwt: Key precisa ter pelo menos 32 caracteres (256 bits).");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
    new Claim(ClaimTypes.Name, usuario.Nome),
    new Claim(ClaimTypes.Email, usuario.Email),
    new Claim(ClaimTypes.Role, usuario.TipoUsuario.NomeTipo),
    new Claim("NIF", usuario.NIF)
};

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiraEmMinutos),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

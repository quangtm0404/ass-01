using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Commons;
using Services.Services.Interfaces;
using Services.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JWTOptions _jwtOptions;
        public TokenGenerator(IOptions<JWTOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(MemberDTO member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email , member.Email),
                new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),


            };
            // Project selected element, convert it into CLaims
            //var role = roles.Select(role => new Claim(ClaimTypes.Role, role));
            claimList.Add(new Claim(ClaimTypes.Role, member.RoleName));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

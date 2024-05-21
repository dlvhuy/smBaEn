using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace SocialMedia.Helper.Implements
{

    public class Token : IToken
    {
        private readonly SociaMediaContext _dbContext;

        private readonly IConfiguration _configuration;

        public Token(SociaMediaContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public string createTokenFormUser(InfoUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = _configuration["AppSetting:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Email,user.EmailUser),

                }),

                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public InfoUser getUserFromToken(string Token)
        {
            if (Token == null) return null;
            if (Token.Contains("Bearer"))
            {
                string[] TokenWithOutBearerString = Token.Split(" ");
                Token = TokenWithOutBearerString[1];
            }

            var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSetting:SecretKey"])),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };

                var tokenValidationResult = handler.ValidateToken(Token, validationParameters, out SecurityToken validatedToken);
                
                var jwtToken = (JwtSecurityToken)validatedToken;

                var claims = jwtToken.Claims;

                string username = claims.First(c => c.Type == "unique_name").Value;

                InfoUser userCurrent = _dbContext.InfoUsers.FirstOrDefault(x => x.UserName == username);

                return userCurrent;
                
            }


        }
    }


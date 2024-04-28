using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Dtos.Requests;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IRegister_SignIn _register_SignIn;
        private readonly IConfiguration _configuration;
        private readonly IToken _tokenFromUser;
        public SignUpController(IRegister_SignIn register_SignIn,IConfiguration configuration, IToken tokenFromUser) {
            _register_SignIn = register_SignIn;
            _configuration = configuration;
            _tokenFromUser = tokenFromUser;
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpRequest signUpRequest) {

            try {
                if(signUpRequest == null) return BadRequest("Null Error");

                
                _register_SignIn.Register(signUpRequest);
                return Ok("succsess");

            }
            catch { return BadRequest("Error "); }
        
        }
        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            try
            {
                if (signInRequest == null) return BadRequest("Null Error");

                InfoUser CheckUser = _register_SignIn.SignIn(signInRequest);
                if (CheckUser == null) return BadRequest("SignIn Error");

                string token = _tokenFromUser.createTokenFormUser(CheckUser);

                InfoUser user = _tokenFromUser.getUserFromToken(token);

                return Ok(user.UserName);

            }
            catch { return BadRequest("Error"); }
        }

        
    }
}

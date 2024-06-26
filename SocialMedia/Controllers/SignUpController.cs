﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Implements;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Implementations;
using SocialMedia.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SocialMedia.Helper.Enums;
namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IRegister_SignIn _register_SignIn;
        public SignUpController(IRegister_SignIn register_SignIn) {
            _register_SignIn = register_SignIn;
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpRequest signUpRequest) {

            try {
                if(signUpRequest == null) return BadRequest("Null Error");

                SignUpResponse status = _register_SignIn.Register(signUpRequest);
                if(!status.success)
                    return BadRequest(status);
                
                return Ok(status);
                

            }
            catch (Exception ex) { return BadRequest(ex); }

        }
        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(SignInRequest signInRequest)
        {
            try
            {
                if (signInRequest == null) return BadRequest("Null Error");

                ResonseLogin CheckUser = _register_SignIn.SignIn(signInRequest);
                if (!CheckUser.success) return BadRequest(CheckUser);
                return Ok(CheckUser);


            }
            catch(Exception ex) { return BadRequest(ex); }
        }

        
    }
}

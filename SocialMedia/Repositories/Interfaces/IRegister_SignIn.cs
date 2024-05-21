using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Enums;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IRegister_SignIn: IDisposable
    {
        SignUpResponse Register(SignUpRequest signUpRequest);

        ResonseLogin SignIn (SignInRequest signUpRequest);


    }
}

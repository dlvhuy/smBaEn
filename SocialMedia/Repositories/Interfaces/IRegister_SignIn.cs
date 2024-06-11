using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;


namespace SocialMedia.Repositories.Interfaces
{
    public interface IRegister_SignIn: IDisposable
    {
        SignUpResponse Register(SignUpRequest signUpRequest);

        ResonseLogin SignIn (SignInRequest signUpRequest);


    }
}

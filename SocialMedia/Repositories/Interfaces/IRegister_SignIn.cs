using SocialMedia.Dtos.Requests;
using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IRegister_SignIn: IDisposable
    {
        bool Register(SignUpRequest signUpRequest);

        InfoUser SignIn (SignInRequest signUpRequest);


    }
}

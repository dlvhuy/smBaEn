using SocialMedia.Models;

namespace SocialMedia.Helper.Interfaces
{
    public interface IToken
    {
        InfoUser getUserFromToken(string Token);

        string createTokenFormUser(InfoUser user); 

    }
}

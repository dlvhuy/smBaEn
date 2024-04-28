using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class Register_SignInRepository : IRegister_SignIn
    {
        private readonly SociaMediaContext _dbcontext;

        private readonly IMapper _mapper;
        public Register_SignInRepository(SociaMediaContext sociaMedia,IMapper mapper) {
            _dbcontext = sociaMedia;
            _mapper = mapper;
        }
        public void Dispose()
        {
            try
            {

            }
            catch { return; }
        }

        public bool Register(SignUpRequest signUpRequest)
        {
            if (signUpRequest == null) return false;
                
            var InfoUser = _mapper.Map<InfoUser>(signUpRequest);

            if(InfoUser == null) return false;  

           

            _dbcontext.InfoUsers.Add(InfoUser);
            _dbcontext.SaveChanges();

            return true;

        }

        public InfoUser SignIn(SignInRequest signUpRequest)
        {
            if( signUpRequest == null) return null;

            InfoUser CheckSignUp = _dbcontext.InfoUsers.Where(info => info.EmailUser == signUpRequest.EmailUser && info.PasswordUser == signUpRequest.PasswordUser).FirstOrDefault();
            if( CheckSignUp == null ) return null;

            return CheckSignUp;

        }

        
    }
}

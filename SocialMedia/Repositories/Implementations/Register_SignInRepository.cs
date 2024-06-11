using AutoMapper;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;

using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Repositories.Implementations
{
    public class Register_SignInRepository : IRegister_SignIn
    { 
        private readonly SociaMediaContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IToken _token;
        private readonly IInforUser _inforUser;


        public Register_SignInRepository(IInforUser inforUser, IToken token,SociaMediaContext sociaMedia,IMapper mapper) {
            _dbcontext = sociaMedia;
            _mapper = mapper;
            _token = token;
            _inforUser = inforUser;
        }
        public void Dispose()
        {
            try { }
            catch { return; }
        }

        public SignUpResponse Register(SignUpRequest signUpRequest)
        {
            if (_dbcontext.InfoUsers.Where(userInfo => userInfo.EmailUser == signUpRequest.EmailUser).Any())
                return new SignUpResponse()
                {
                    success = false,
                    message = "Already had EmailUser"
                };

            if (_dbcontext.InfoUsers.Where(userInfo => userInfo.UserName == signUpRequest.UserName).Any())
                return new SignUpResponse()
                {
                    success = false,
                    message = "Already had UserName"
                };

            var InfoUser = _mapper.Map<InfoUser>(signUpRequest);
            bool isSuccess  = _inforUser.CreateUser(InfoUser); 

            if(!isSuccess) return new SignUpResponse()
            {
                success = false,
                message = "Other Error"
            };


            return new SignUpResponse()
            {
                success = true,
                message = "Register success"
            };

        }

        public ResonseLogin SignIn(SignInRequest signUpRequest)
        {
            if( signUpRequest == null) return null;

            InfoUser CheckSignUp = _dbcontext.InfoUsers.Where(info => info.EmailUser == signUpRequest.EmailUser && info.PasswordUser == signUpRequest.PasswordUser).FirstOrDefault();
            if( CheckSignUp == null )
                return new ResonseLogin
            {
                success = false,
                idCurrentUser = 0,
                Token = "",
                Message = "Lỗi sai Email hoặc mật khẩu"
            }; ;

            string token = _token.createTokenFormUser(CheckSignUp);
            int userId = _token.getUserFromToken(token).IdUser;

            return new ResonseLogin
            {
                success = true,
                idCurrentUser = userId,
                Token = token,
                Message =""
            };

        }

        
    }
}

namespace SocialMedia.Helper.Enums
{
    public class SignIn_SignUpStatus
    {
        public enum SignUpStatus
        {
            Success,
            Fail_Arealdy_Had_UserName,
            Fail_Arealdy_Had_EmailUser,
            Fail_Arealdy_Had_PhoneNumber,
            OtherError
        }

        public enum SignIpStatus
        {
            Success ,
            Fail_PassWord_Or_EmailUser_Incorrect
           ,
        }
    }
}

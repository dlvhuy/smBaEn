using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IInforUser _inforUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly ILikePost _likePost;

        public UserInfoController(ILikePost likePost, IInforUser infoUser, IToken token, IHttpContextAccessor httpContextAccessor) { 
            
            _inforUser = infoUser;
            _token = token;
            _httpContextAccessor = httpContextAccessor;
            _likePost = likePost;
        }

        [HttpGet("{idUser}")]
        [Authorize]
        public ActionResult Index(int idUser) {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int idUserCurrent = _token.getUserFromToken(token).IdUser;
                var infoUserResponse = _inforUser.GetInfomationInUser(idUser, idUserCurrent);

                return Ok(new MainResponse(infoUserResponse, true));
            }
            catch
            {
                return BadRequest(new MainResponse(null, false));
            }
                
        }
        [HttpPost("{SearchString}")]
        [Authorize]
        public ActionResult GetSearchUserInfo(string SearchString) {

            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                InfoUser userCurrent = _token.getUserFromToken(token);
                var infoUserResponse = _inforUser.SearchUser(SearchString, userCurrent);

                return Ok(new MainResponse(infoUserResponse, true));
            }
            catch
            {
                return BadRequest(new MainResponse(null, false));
            }


        }
    }
}

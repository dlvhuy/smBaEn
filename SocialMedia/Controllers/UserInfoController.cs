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
                InfoUser userCurrent = _token.getUserFromToken(token);

                InfoUserResponse infoUserResponse = _inforUser.GetInfomationInUser(idUser, userCurrent.IdUser);
                foreach (var post in infoUserResponse.PostResponses)
                {
                    post.LikePost.isLike = _likePost.GetIsUserLikePost(post.IdPost, userCurrent.IdUser);
                }

                return Ok(new MainResponse
                {
                    Object = infoUserResponse,
                    success = true,
                });

            }
            catch
            {
                return BadRequest(new MainResponse
                {
                    Object = null,
                    success = false,
                });
            }
                
        }
        [HttpPost("{SearchString}")]
        [Authorize]
        public ActionResult GetSearchUserInfo(string SearchString) {

            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                InfoUser userCurrent = _token.getUserFromToken(token);

                IEnumerable<ItemSearchUser> infoUserResponse = _inforUser.SearchUser(SearchString, userCurrent);

                return Ok(new MainResponse
                {
                    Object = infoUserResponse,
                    success = true,
                });

            }
            catch
            {
                return BadRequest(new MainResponse
                {
                    Object = null,
                    success = false,
                });
            }


        }
    }
}

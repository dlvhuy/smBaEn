using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Hubs.ImplementHubs;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Controllers
{   // thêm tính năng hình ảnh 
    // thêm bạn bè , gợi ý bạn bè 
    //thêm tính năng nhóm
    // thêm redis
    // học thêm về thuật toán
    // học thêm về system design
    // redesgin app mobileh
    //elasticSearch cho SignUPCOntroller

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _post;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly ILikePost _likepost;
        public PostController(ILikePost likepost, IHubContext<PostHub> hubContext, IPost post, IHttpContextAccessor httpContextAccessor, IToken token)
        {
            _likepost = likepost;
            _post = post;
            _httpContextAccessor = httpContextAccessor;
            _token = token;
            
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeletePost(int idPost)
        {
            try
            {
                if (idPost == null) return BadRequest("Null Error");

                if (!_post.DeletePost(idPost)) return BadRequest("Error");

                return Ok("Success");
            }
            catch { return BadRequest("Error"); }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetPosts() {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]; 
                int UserId = _token.getUserFromToken(token).IdUser;
                var listPost = _post.GetAllPosts();

                foreach(var post in listPost) {
                    post.LikePost.isLike = _likepost.GetIsUserLikePost(post.IdPost, UserId);
                }
                return Ok(listPost);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Requests;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Hubs.ImplementHubs;

using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _post;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly IHubContext<PostHub> _hubContext;
        public PostController(IHubContext<PostHub> hubContext, IPost post,IHttpContextAccessor httpContextAccessor,IToken token)
        {
            _post = post;
            _httpContextAccessor = httpContextAccessor;
            _token = token;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddPost(CreatePostRequest createPostRequest)
        {
            try
            {
                if (createPostRequest == null) return BadRequest("Null Error");

                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                InfoUser user = _token.getUserFromToken(token);

                if (user == null) return BadRequest("Null Error");

                if (!_post.AddPost(user.IdUser, createPostRequest)) return BadRequest("Error");

                return Ok("success");
                

            }
            catch { return BadRequest("Error"); }
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
        public IActionResult GetPosts() {
            try
            {
                List<PostResponse> postResponse = new List<PostResponse>();

                PostResponse postResponse1 = new PostResponse();
                postResponse1.IdGroup = 1;
                postResponse1.IdPost = 1;
                postResponse1.IdUser = 1;

                PostResponse postResponse2 = new PostResponse();
                postResponse1.IdGroup = 2;
                postResponse1.IdPost = 2;
                postResponse1.IdUser = 2;

                PostResponse postResponse3 = new PostResponse();
                postResponse1.IdGroup = 3;
                postResponse1.IdPost = 3;
                postResponse1.IdUser = 3;

                postResponse.Add(postResponse1);
                postResponse.Add(postResponse2);
                postResponse.Add(postResponse3);

                

                return Ok("success");
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

    }
}

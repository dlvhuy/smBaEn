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
    // modular monoliths
    // avoid n+1
    //multi tenants (1 tenatls 1 database)

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _post;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly ILikePost _likepost;
        private readonly IHubContext<PostHub> _hubContext;
        public PostController(ILikePost likepost, IHubContext<PostHub> hubContext, IPost post, IHttpContextAccessor httpContextAccessor, IToken token)
        {
            _likepost = likepost;
            _post = post;
            _httpContextAccessor = httpContextAccessor;
            _token = token;
            _hubContext = hubContext;


        }

        [HttpGet]
        [Authorize]
        
        public IActionResult GetPosts() {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;
                var listPost = _post.GetAllPosts();

                foreach (var post in listPost) {
                    post.LikePost.isLike = _likepost.GetIsUserLikePost(post.IdPost, UserId);
                }
                return Ok(listPost);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(CreatePostRequest createPostRequest)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;

                PostResponse postResponse = _post.AddPost(UserId, createPostRequest);

                if (postResponse != null)
                {
                    MainResponse mainResponse = new MainResponse
                    {
                        Object = postResponse,
                        success = true,
                    };
                    await _hubContext.Clients.All.SendAsync("ReceiveMessagePost", mainResponse);
                    return (Ok());
                }
                else
                {
                    MainResponse mainResponse = new MainResponse
                    {
                        Object = null,
                        success = false,
                    };
                    return BadRequest(mainResponse);
                }
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpGet("{idPost}")]
        [Authorize]
        public IActionResult GetPost(int idPost)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;
                PostResponse post = _post.GetPost(idPost);
                post.LikePost.isLike = _likepost.GetIsUserLikePost(post.IdPost, UserId);

                MainResponse mainResponse = new MainResponse
                {
                    Object = post,
                    success = true,
                };
                return Ok(mainResponse);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

    }
}

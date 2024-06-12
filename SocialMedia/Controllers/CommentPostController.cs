using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using SocialMedia.Services.CommentService;
using SocialMedia.Services.CommentService.Dtos.Request;
using System.Net.WebSockets;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentPostController : ControllerBase
    {
        private readonly ICommentPostService _commentPostService;
        private readonly IToken _token;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentPostController(IHttpContextAccessor httpContextAccessor, IToken token,ICommentPostService commentPostService)
        {
            _commentPostService = commentPostService;
            _httpContextAccessor = httpContextAccessor;
            _token = token;
        }
        [HttpGet("{idPost}")]
        public IActionResult GetCommentsPost(int idPost)
        {
            try
            {
                var listPost = _commentPostService.GetCommentInPost(idPost);
                var mainResonse = new MainResponse(listPost.Reverse(), true);
                return Ok(mainResonse);
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddCommentPost(CommentPostRequest commentPostRequest)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;
                _commentPostService.AddCommentPost(UserId,commentPostRequest);
                
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
    }
}

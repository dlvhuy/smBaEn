﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Respones;
using SocialMedia.Helper.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services.PostService.Dtos.Request;
using SocialMedia.Services.PostService.Dtos.Response;
using SocialMedia.Services.PostService;
using SocialMedia.Hubs;

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
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToken _token;
        private readonly IPostService _postService;
        
       
        public PostController(IPostService postService,IHttpContextAccessor httpContextAccessor, IToken token)
        { 
            _httpContextAccessor = httpContextAccessor;
            _token = token;
            _postService = postService;
            
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetPostsInUser() {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;
                var listPost = _postService.GetPosts(UserId);

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
                var response = _postService.AddPost(UserId, createPostRequest);

                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex); }
        }

        [HttpGet("{idPost}")]
        [Authorize]
        public IActionResult UpdateLikePost(int idPost)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                int UserId = _token.getUserFromToken(token).IdUser;
                _postService.UpdateLikePost(UserId, idPost);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex); }
        }
    }
}

using Microsoft.AspNetCore.Http.Features;
using SocialMedia.Helper.Interfaces;
using System.Net.Http;

namespace SocialMedia.Helper.Implements
{
    public class ManageImage : IManageImage
    {
        private readonly IHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ManageImage(IHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public string ChangeNameFileToURL(string imageName)
        {
            return String.Format("{0}://{1}{2}/Image/", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host, _httpContextAccessor.HttpContext.Request.PathBase, imageName);
        }

        public string GetDefaultAvatarImage()
        {
          
            return "Image/Default_AvatarImage.jpg";
        }

        public string GetDefaultCoverImage()
        {
           
            return "Image/Default_Image.jp";
        }
        public string SetDefaultCoverImage()
        {
            return String.Format("{0}://{1}{2}/Image/Default_Image.jpg", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host, _httpContextAccessor.HttpContext.Request.PathBase);
        }

        public string SetDefaultAvatarImage()
        {
            return String.Format("{0}://{1}{2}/Image/Default_AvatarImage.jpg", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host, _httpContextAccessor.HttpContext.Request.PathBase);
        }

        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_env.ContentRootPath, "Image",imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);   
            }    
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(_env.ContentRootPath, "Image ", imageName);

            using(var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imageName;
        }

    }
}

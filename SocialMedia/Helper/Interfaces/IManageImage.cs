using SocialMedia.Services.PostService.Dtos.Request;
using System.Threading.Tasks;

namespace SocialMedia.Helper.Interfaces
{
    public interface IManageImage
    {
        Task<string> SaveImage(IFormFile file);

        string SaveImage64Base(PostContentRequest postContentRequest);

        void DeleteImage(string imageName);

        string ChangeNameFileToURL(string imageName);

        public string GetDefaultCoverImage();

        public string GetDefaultAvatarImage();

        public string SetDefaultCoverImage();

        public string SetDefaultAvatarImage();
    }
}

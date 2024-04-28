namespace SocialMedia.Helper.Interfaces
{
    public interface IManageImage
    {
        Task<string> SaveImage(IFormFile file);

        void DeleteImage(string imageName);

        string ChangeNameFileToURL(string imageName);
    }
}

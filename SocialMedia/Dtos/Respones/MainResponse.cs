namespace SocialMedia.Dtos.Respones
{
    public class MainResponse
    {
        public MainResponse()
        {
           
        }
        public MainResponse(object Object,bool success)
        {
            this.Object = Object;
            this.success = success;
        }
        public bool success { get; set; }

        public object Object { get; set; }
    }
}

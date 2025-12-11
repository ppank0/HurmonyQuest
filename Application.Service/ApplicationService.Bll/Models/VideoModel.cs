using ApplicationService.BLL.Interfaces;

namespace ApplicationService.BLL.Models
{
    public class VideoModel : IBaseModel
    {
        public string VideoUrl {  get; set; } = string.Empty;
    }
}

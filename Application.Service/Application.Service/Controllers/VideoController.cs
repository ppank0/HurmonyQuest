using ApplicationService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController(IVideoService videoService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetObjectAsync(string name, CancellationToken ct)
        {
<<<<<<< feature/9-create-integration-tests
            var (stream, contentType) = await videoService.GetAsync(name, ct);
            return File(stream, contentType);
=======
            var file = await videoService.GetAsync(name, ct);
            return File(file.Stream, file.ContentType);
>>>>>>> main
        }

        [HttpPost]
        public async Task UploadObjectAsync(IFormFile file, CancellationToken ct)
        {
            await using var stream = file.OpenReadStream();
            await videoService.PutAsync(file.FileName, file.ContentType, stream, ct);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            await videoService.DeleteAsync(id, ct);
        }
    }
}

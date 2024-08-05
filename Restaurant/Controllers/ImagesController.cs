using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController(IConfiguration configuration) : ControllerBase
    {
        string ProductImgPath = configuration["FolderPath:Product"]!;

        [HttpGet("products")]
        public string GetFullProductImgPath()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/";
            var imageUrl = $"{baseUrl}{ProductImgPath.TrimStart('/')}";

            return imageUrl;
        }



    }
}

using GMS.Core.Services;

namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostFileAsync(IFormFile image)
        {
            string filePath = "";
            try{
                filePath = await FileService.SaveFileAsync(image, _env.ContentRootPath, "GymImages");
                return filePath;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return StatusCode(400, new Response{Status="Error", Message="File save gone wrong bro"});
        }
    }
}
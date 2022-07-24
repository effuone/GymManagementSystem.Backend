using GMS.Core.Services;
using GMS.Data.DTOs;

namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("api/gyms")]
    public class GymController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<GymController> _logger;
        private readonly IWebHostEnvironment _env;

        public GymController(IGymRepository gymRepository, ILocationRepository locationRepository, ILogger<GymController> logger, IWebHostEnvironment env)
        {
            _gymRepository = gymRepository;
            _locationRepository = locationRepository;
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public async Task<IEnumerable<Gym>> GetGymsAsync()
        {
            return await _gymRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Gym>> GetGymAsync(int id)
        {
            var existingModel = await _gymRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("Gym not found");
            }
            var dto = new GymDTO();
            dto.GymName = existingModel.GymName;
            dto.ImageFilePath = existingModel.Image;
            dto.LocationId = existingModel.LocationId;
            return existingModel;
        }
        [HttpPost]
        public async Task<ActionResult<Gym>> CreateGymAsync([FromForm]CreateGymDTO gym)
        {
            var existingGym = await _gymRepository.GetAsync(gym.GymName);
            if(existingGym is not null)
            {
                return BadRequest("This gym already exists");
            }
            var model = new Gym();

            model.GymName = gym.GymName;
            model.LocationId = gym.LocationId;
            if(gym.Image.Length > 0)
            {
                var filePath = await FileService.SaveFileAsync(gym.Image, _env.ContentRootPath, "/GymImages/");
                if(!filePath.IsNullOrEmpty())
                    model.Image = filePath;
            }
            var id = await _gymRepository.CreateAsync(model);
            if(id <= 0)
                return BadRequest("Creation failed");
            return CreatedAtAction(nameof(GetGymAsync), new { id = id }, model);
        }
    }
}
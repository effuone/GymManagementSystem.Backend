namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityRepository repository, ILogger<CityController> logger)
        {
            _cityRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityAsync(int id)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("City not found");
            }
            return existingModel;
        }
        [HttpPost]
        public async Task<ActionResult<City>> PostCityAsync([Required]string cityName)
        {
            var existingModel = await _cityRepository.GetAsync(cityName);
            if(existingModel is not null)
            {
                return BadRequest("City already exists");
            }
            var model = new City();
            model.CityName = cityName;
            model.CityId = await _cityRepository.CreateAsync(model);
             return CreatedAtAction(nameof(GetCityAsync), new { id = model.CityId }, model);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UdpateCityAsync(int id, [Required] string cityName)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("City not found");
            }
            await _cityRepository.UpdateAsync(id, new City(){CityName = cityName});
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCityAsync(int id)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("City not found");
            }
            await _cityRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
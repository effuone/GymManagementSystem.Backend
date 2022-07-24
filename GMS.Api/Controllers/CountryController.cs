using Microsoft.AspNetCore.Authorization;

namespace GMS.Api.Controllers
{
    [ApiController]
    // [Authorize(Roles = "Admin")]
    [Route("api/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository repository, ILogger<CountryController> logger)
        {
            _countryRepository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryAsync(int id)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("Country not found");
            }
            return existingModel;
        }
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountryAsync([Required]string countryName)
        {
            var existingModel = await _countryRepository.GetAsync(countryName);
            if(existingModel is not null)
            {
                return BadRequest("Country already exists");
            }
            var model = new Country();
            model.CountryName = countryName;
            model.CountryId = await _countryRepository.CreateAsync(model);
             return CreatedAtAction(nameof(GetCountryAsync), new { id = model.CountryId }, model);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UdpateCountryAsync(int id, [Required] string countryName)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("Country not found");
            }
            await _countryRepository.UpdateAsync(id, new Country(){CountryName = countryName});
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountryAsync(int id)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound("Country not found");
            }
            await _countryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
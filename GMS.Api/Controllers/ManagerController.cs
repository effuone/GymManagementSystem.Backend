using AutoMapper;
using GMS.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IManagerTypeRepository _managerTypeRepository;
        private readonly IGymRepository _gymRepository;
        private readonly ILogger<ManagerController> _logger;
        private readonly IMapper _mapper;

        public ManagerController(IManagerRepository managerRepository, IManagerTypeRepository managerTypeRepository, IGymRepository gymRepository, ILogger<ManagerController> logger, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _managerTypeRepository = managerTypeRepository;
            _gymRepository = gymRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Manager>> GetManagersAsync()
        {
            return await _managerRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerDTO>> GetManagerAsync(int id)
        {
            var existingModel = await _managerRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound();
            }
            var managerReadDTO = _mapper.Map<ManagerDTO>(existingModel);
            return Ok(managerReadDTO);
        }
        // [HttpPost]
        // public async Task<ActionResult<Manager>> CreateManagerAsync(Manager manager)
        // {
            
        // }
        [Authorize("Admin")]
        [HttpPost("/api/managerTypes/")]
        public async Task<ActionResult<ManagerType>> CreateManagerTypeAsync([Required]string managerType)
        {
            var id = await _managerTypeRepository.CreateAsync(new ManagerType{ManagerTypeName = managerType});
            return StatusCode(200,new ManagerType{ManagerTypeId=id, ManagerTypeName=managerType});
        }
        [HttpPost]
        public async Task<ActionResult<Manager>> CreateManagerAsync(CreateManagerDTO managerDTO)
        {
            var model = new Manager();
            var managerType = await _managerTypeRepository.GetAsync(managerDTO.ManagerType);
            if(managerType is null)
                return BadRequest("Manager type not found");
            model.ManagerType = managerType;
            var gym = await _gymRepository.GetAsync(managerDTO.GymId);
            if(gym is null)
                return BadRequest("Gym not found");
            model.Gym = gym;
            model.GMSUserId = managerDTO.GMSUserId;
            var id = await _managerRepository.CreateAsync(model);
            // return CreatedAtAction(nameof(GetManagerAsync), new { id = id }, model);
            return StatusCode(200);
        }
    }
}
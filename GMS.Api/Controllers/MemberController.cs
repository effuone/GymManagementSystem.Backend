namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : ControllerBase
    {
        private readonly GMSAppContext _context;
        private readonly ILogger<MemberController> _logger;

        public MemberController(GMSAppContext context, ILogger<MemberController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using GMS.Data.AuthModels;
using Newtonsoft.Json;
using System.Net;
namespace GMS.Api.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly GMSAppContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberRepository _memberRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IManagerRepository _managerRepository;
        // private readonly IGymRepository _portfolioRepository;
        private readonly UserManager<GMSUser> _userManager;
        private readonly SignInManager<GMSUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(GMSAppContext dbContext, RoleManager<IdentityRole> roleManager, IMemberRepository memberRepository, ICoachRepository coachRepository, IManagerRepository managerRepository, UserManager<GMSUser> userManager, SignInManager<GMSUser> signInManager, IConfiguration configuration, ILogger<AccountController> logger)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _memberRepository = memberRepository;
            _coachRepository = coachRepository;
            _managerRepository = managerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }
        private async Task<JwtSecurityToken> EncodeToken(GMSUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < userRoles.Count; i++)
            {
                var claim = new Claim($"Role", userRoles[i]);
                roleClaims.Add(claim);
                if (userRoles[i] == "Manager")
                {
                    var manager = await _managerRepository.GetAsync(user.Id);
                    var managerClaim = new Claim("ManagerId", manager.ManagerId.ToString());
                    roleClaims.Add(managerClaim);
                }
                if (userRoles[i] == "Coach")
                {
                    var coach = await _coachRepository.GetAsync(user.Id);
                    var coachClaim = new Claim("CoachId", coach.CoachId.ToString());
                    roleClaims.Add(coachClaim);
                }
                if (userRoles[i] == "Member")
                {
                    var member = await _memberRepository.GetAsync(user.Id);
                    var memberClaim = new Claim("MemberId", member.MemberId.ToString());
                    roleClaims.Add(memberClaim);
                }
            }
            var authClaims = new List<Claim>
                {
                    new Claim("Username", user.UserName),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Gender", user.Gender ? "Male" : "Female"),
                    new Claim("Birthday", user.Birthday.ToShortDateString()),
                    new Claim("Email", user.Email),
                    new Claim("PhoneNumber", user.PhoneNumber)
                };
            authClaims.AddRange(roleClaims);
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));

            var token = new JwtSecurityToken
            (
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public List<string> DecodeJWTToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var claims = new List<string>();
            foreach (var claim in tokenS.Claims)
            {
                claims.Add(claim.Value);
            }
            return claims;
        }
        [AllowAnonymous]
        [HttpPost("role")]
        public async Task<ActionResult<string>> CreateRoleAsync([Required] string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                return NotFound("Role already exist");
            }
            await _roleManager.CreateAsync(new IdentityRole(role));
            return StatusCode(200, new Response { Status = "Success", Message = "Role created successfully" });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync([FromBody] RegisterModelType registerModel)
        {
            //Check if user exists
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }
            //Create new user
            GMSUser user = new GMSUser()
            {
                UserName = registerModel.FirstName + registerModel.LastName,
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                PhoneNumber = registerModel.PhoneNumber,
                Birthday = registerModel.Birthday,
                Gender = registerModel.Gender
            };
            //Check role validity        
            if (await _roleManager.RoleExistsAsync(registerModel.Role) is false)
            {
                return NotFound("Role not found");
            }
            //Update new user in database
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                return StatusCode(200, new Response{Status="Success", Message="Registered successfully"});
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginModelType loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Wrong password or email" });
            }
            // if(user.EmailConfirmed is false)
            // {
            //     return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Confirm your email first" });   
            // }  
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var token = await EncodeToken(user);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
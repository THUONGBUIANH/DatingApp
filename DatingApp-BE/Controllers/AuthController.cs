using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp_BE.Data;
using DatingApp_BE.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetAppAPIAngular.API.Models;

namespace DatingApp_BE.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository authRepository,
         IConfiguration config
        )
        {
            _authRepository = authRepository;
            _config = config;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _authRepository.UserExists(userForRegisterDto.Username))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromLogin = await _authRepository.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromLogin == null)
                return Unauthorized();

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, userFromLogin.Id.ToString()),
               new Claim(ClaimTypes.Name, userFromLogin.Username)
            };

            var key  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]));

            return Ok();
        }
    }
}
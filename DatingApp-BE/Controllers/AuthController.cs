using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp_BE.Data;
using Microsoft.AspNetCore.Mvc;
using PetAppAPIAngular.API.Models;

namespace DatingApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

         [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (await _authRepository.UserExists(username.ToLower()))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                Username = username
            };

            var createdUser = await _authRepository.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}
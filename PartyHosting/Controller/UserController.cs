using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyHosting.Data;
using PartyHosting.Models;

namespace PartyHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Party_db_context context;

        public UserController(Party_db_context context)
        {
            this.context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var existing = await context.Users.FirstOrDefaultAsync(u=> u.Username == user.Username || u.Email == user.Email);
            if(existing!=null)
            {
                return BadRequest("Username or email already exists...try different one please!");
            }
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Email== email);

            return Ok(user.Id);
        }

        [HttpGet("getusername")]
        public async Task<IActionResult> GetUsername(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Email== email);

            return Ok(user.Username);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == request.Password);

            if (user != null)
            {
                return Ok("Login Successful");
            }
            else
            {
                return Unauthorized("Invalid Credentials");
            }
        }
        
        public class LoginRequest
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}

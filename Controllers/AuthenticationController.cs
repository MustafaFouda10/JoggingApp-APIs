using JoggingApp.Data;
using JoggingApp.Models;
using JoggingApp.Options;
using JoggingApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace JoggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JoggingDbContext joggingDbContext;
        public AuthenticationController(JoggingDbContext joggingDbContext)
        {
            this.joggingDbContext = joggingDbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState); // error 400
            }

            User user = new User
            {
                UserName = registerViewModel.UserName,
                Age = registerViewModel.Age,
                Password = registerViewModel.Password,
                RoleId = 2
            };

            await joggingDbContext.Set<User>().AddAsync(user);
            await joggingDbContext.SaveChangesAsync();
            return Ok(); // 200
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState); // error 400
            }

            User loginUser = joggingDbContext.Set<User>().Include(u => u.Role)
                            .FirstOrDefault(u => u.UserName == loginViewModel.UserName && u.Password == loginViewModel.Password);

            if (loginUser != null)
            {
                string token = GenerateToken(loginUser.Id, loginUser.Role.Name);
                loginUser.IsLogout = false;
                joggingDbContext.SaveChanges();
                return Ok(token);
            }
            return BadRequest("Check your userName or Password");
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult logout()
        {

            int userId = int.Parse(HttpContext.GetUserId());
            User user = joggingDbContext.Set<User>().FirstOrDefault(u => u.Id == userId);

            user.IsLogout = true;
            joggingDbContext.SaveChanges();

            return Ok("Logout Successfully");
        }

        private string GenerateToken(int userId, string role)
        {

            var issuer = "https://joydipkanjilal.com/";
            var audience = "https://joydipkanjilal.com/";
            var key = Encoding.UTF8.GetBytes("adkaksmasmfaskfmaskfmqwfmqmgo124mqmfqqfqwsana");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                //new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Id", userId.ToString()),
                new Claim(ClaimTypes.Role, role)

             }),
                Expires = DateTime.UtcNow.AddHours(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
    }
}

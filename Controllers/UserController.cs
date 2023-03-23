using JoggingApp.Data;
using JoggingApp.Models;
using JoggingApp.Options;
using JoggingApp.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JoggingApp.Controllers
{
    [ServiceFilter(typeof(ActionFilterExample))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User Manager")]
    public class UserController : ControllerBase
    {
        private readonly JoggingDbContext joggingDbContext;

        public UserController(JoggingDbContext joggingDbContext)
        {
            this.joggingDbContext = joggingDbContext;
        }


        [HttpPost]
        public IActionResult Create(AddUser addUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User();

                user.UserName = addUser.UserName;
                user.Age = addUser.Age;
                user.Password = addUser.Password;
                user.RoleId = 3;

                joggingDbContext.Set<User>().Add(user);
                joggingDbContext.SaveChanges();
                return Ok("Saved Successfully");
            }

            return BadRequest("Failure! Please check again");
        }


        [HttpGet]
        [Route("GetAll")]
        public List<User> GetAll()
        {
            List<User> userList = joggingDbContext.Set<User>().ToList();
            return userList;
        }


        [HttpGet]
        [Route("GetById")]
        public User GetById(int userId)
        {
            User user = joggingDbContext.Set<User>().FirstOrDefault(j => j.Id == userId);
            return user;
        }


        [HttpPut]
        public IActionResult Update(int userId, AddUser newUser)
        {
            if (ModelState.IsValid)
            {
                User oldUser = joggingDbContext.Set<User>().FirstOrDefault(j => j.Id == userId);

                if (oldUser != null)
                {
                    oldUser.UserName = newUser.UserName;
                    oldUser.Age = newUser.Age;
                    oldUser.Password = newUser.Password;

                    joggingDbContext.SaveChanges();
                    return Ok("Updated Successfully");
                }

            }

            return BadRequest("Please, Check your updates again");
        }

        [HttpDelete]
        public IActionResult Delete(int userId)
        {
            User user = joggingDbContext.Set<User>().FirstOrDefault(c => c.Id == userId);
            if (user != null)
            {
                joggingDbContext.Set<User>().Remove(user);
                joggingDbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }

            return BadRequest("Failure! Please Check again");
        }
    }
}

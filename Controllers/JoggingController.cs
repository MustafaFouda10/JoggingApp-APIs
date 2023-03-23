using JoggingApp.Data;
using JoggingApp.Models;
using JoggingApp.Options;
using JoggingApp.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoggingApp.Controllers
{
    [ServiceFilter(typeof(ActionFilterExample))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Regular User")]
    public class JoggingController : ControllerBase
    {
        private readonly JoggingDbContext joggingDbContext;

        public JoggingController(JoggingDbContext joggingDbContext)
        {
            this.joggingDbContext = joggingDbContext;
        }


        [HttpPost]
        public IActionResult Create(AddJogging addJogging)
        {
            if (ModelState.IsValid)
            {
                Jogging jogging = new Jogging();
                int userId = int.Parse(HttpContext.GetUserId());
                jogging.UserId = userId;
                jogging.Date = addJogging.Date;
                jogging.Time = addJogging.Time;
                jogging.Distance = addJogging.Distance;

                joggingDbContext.Set<Jogging>().Add(jogging);
                joggingDbContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("GetAll")]
        public List<Jogging> GetAll()
        {
            List<Jogging> joggingList = joggingDbContext.Set<Jogging>().ToList();
            return joggingList;
        }


        [HttpGet]
        [Route("GetById")]
        public Jogging GetById(int joggingId)
        {
            Jogging jogging = joggingDbContext.Set<Jogging>().FirstOrDefault(j => j.Id == joggingId);
            return jogging;
        }


        [HttpPut]
        public IActionResult Update(int joggingId, AddJogging newJogging)
        {
            if (ModelState.IsValid)
            {
                Jogging oldJogging = joggingDbContext.Set<Jogging>().FirstOrDefault(j => j.Id == joggingId);

                if (oldJogging != null)
                {
                    oldJogging.Distance = newJogging.Distance;
                    oldJogging.Date = newJogging.Date;
                    oldJogging.Time = newJogging.Time;

                    joggingDbContext.SaveChanges();
                    return Ok("Updated Successfully");
                }

            }

            return BadRequest("Please, Check your updates again");
        }

        [HttpDelete]
        public IActionResult Delete(int joggingId)
        {
            Jogging jogging = joggingDbContext.Set<Jogging>().FirstOrDefault(c => c.Id == joggingId);
            if (jogging != null)
            {
                joggingDbContext.Set<Jogging>().Remove(jogging);
                joggingDbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }

            return BadRequest("Failure! Please Check again");
        }

        [HttpGet]
        [Route("FilterJogging")]
        public IActionResult FilterJogging(DateTime fromTime, DateTime toTime)
        {
            List<Jogging> joggingList = joggingDbContext.Set<Jogging>()
                .Where(j=>j.Date.Date >= fromTime.Date && j.Date.Date <= toTime.Date).ToList();

            List<FilterJoggingViewModel> filterJogging = new List<FilterJoggingViewModel>();

            foreach(var item in joggingList)
            {
                filterJogging.Add(new FilterJoggingViewModel { Date= item.Date , Distance = item.Distance, Time = item.Time});
            }

            return Ok(filterJogging);
        }

        [HttpGet]
        [Route("JoggingReport")]
        public IActionResult JoggingReport()
        {
            JoggingReportViewModel joggingReport = new JoggingReportViewModel();

            joggingReport.AvgSpeed = joggingDbContext.Set<Jogging>().Sum(j => j.Distance / j.Time.TotalHours) / 7;
            joggingReport.AvgDistance = joggingDbContext.Set<Jogging>().Sum(j => j.Distance) / 7;

            return Ok(joggingReport);
        }
    }
}

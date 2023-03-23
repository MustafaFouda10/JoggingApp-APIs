using JoggingApp.Data;
using JoggingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace JoggingApp.Options
{
    public class ActionFilterExample : IActionFilter
    {
        private readonly JoggingDbContext joggingDbContext;

        public ActionFilterExample(JoggingDbContext joggingDbContext)
        {
            this.joggingDbContext = joggingDbContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            GeneralExtentions.GetUserId(context.HttpContext);
            int userId = int.Parse(GeneralExtentions.GetUserId(context.HttpContext));
            bool isLogout = joggingDbContext.Set<User>().Any(u => u.Id == userId && u.IsLogout);

            if (isLogout)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
